﻿using Microsoft.Data.Sqlite;
using Rezeptify.AppComponents.Models;
using System.Collections.ObjectModel;

namespace Rezeptify.AppComponents;

public static class DatabaseHandler
{
    // Datenbankverbindung eröffnen & Tabellen erstellen falls Sie nicht existieren
    public static SqliteConnection OpenDatabaseConnection()
    {
        IFileManager? service = Components.GetService<IFileManager>();
        string databasePath = Path.Combine(service.GetApplicationFolder(), "rezeptify.db");

        // Variablen
        SqliteConnection conn = null;

        try
        {
            if (!File.Exists(databasePath)) { File.Create(databasePath); }

            conn = new($"Data Source={databasePath}");

            conn.Open();

            // if - Datenbankconnection nicht eröffnet -> Fehler werfen
            if (conn.State != System.Data.ConnectionState.Open) throw new Exception("Verbindung zur Datenbank fehlgeschlagen!");

            // recipe-Tabelle erstellen falls sie nicht existiert
            SqliteCommand create_recipe = conn.CreateCommand();
            create_recipe.CommandText = "CREATE TABLE IF NOT EXISTS recipe (id INTEGER NOT NULL, instructions TEXT NULL, name TEXT NULL, PRIMARY KEY (id));";
            create_recipe.ExecuteNonQuery();

            SqliteCommand create_ingredients = conn.CreateCommand();
            create_ingredients.CommandText = "CREATE TABLE IF NOT EXISTS ingredients (id INTEGER NOT NULL, name TEXT NOT NULL, quantity REAL NULL, unit VARCHAR(50) NULL, PRIMARY KEY (id));";
            create_ingredients.ExecuteNonQuery();

            //SqliteCommand create_recipeingredients = conn.CreateCommand();
            //create_recipeingredients.CommandText = "CREATE TABLE IF NOT EXISTS recipeingredients (recipe_id INTEGER NOT NULL, ingredient_id INTEGER NOT NULL, CONSTRAINT '0' FOREIGN KEY (ingredient_id) REFERENCES ingredients (id) ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT '1' FOREIGN KEY (recipe_id) REFERENCES recipe (id) ON UPDATE NO ACTION ON DELETE NO ACTION);";
            //create_recipeingredients.ExecuteNonQuery();

            SqliteCommand create_eancodes = conn.CreateCommand();
            create_eancodes.CommandText = "CREATE TABLE IF NOT EXISTS eancodes ( id INTEGER NOT NULL, eancode VARCHAR(255) NOT NULL, name VARCHAR(255) NOT NULL, PRIMARY KEY (id));";
            create_eancodes.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return conn;
    }

    // Vorhandene Zutaten aus der Datenbank laden
    public static List<Ingredients> LoadIngredients(SqliteConnection conn)
    {
        // Variablen
        List<Ingredients> ingredients = new();

        try
        {
            SqliteCommand loadIng_command = new("SELECT id, name, quantity, unit from ingredients", conn);
            SqliteDataReader reader = loadIng_command.ExecuteReader();

            // while - Daten werden gelesen
            while (reader.Read())
            {
                // temporäre Ingredient erstellen und Daten lesen
                Ingredients temp = new()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Quantity = reader.GetDouble(2),
                    Unit = reader.GetString(3)
                };

                // Ingredient zur Liste hinzufügen
                ingredients.Add(temp);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Liste der Zutaten zurückgeben
        return ingredients;
    }

    // neue Zutat hinzufügen
    public static async Task AddIngredients(string name, double quantity, string unit, string ean, SqliteConnection conn)
    {
        // Variablen
        int? ingredient_id = null;
        string name_availiable = null;
        string unit_availiable = null;
        bool already_added = false;

        try
        {
            // Prüfen ob eine Menge > 0 hinzugefügt wird
            if (quantity < 0) return;

            // Prüfen ob das selbe Produkt aktuell in der Datenbank ist
            if (!string.IsNullOrEmpty(ean))
            {
                SqliteCommand test_available = new("SELECT name, unit FROM ingredients WHERE name = @name AND (SELECT eancode from eancodes WHERE eancode = @ean)", conn);
                test_available.Parameters.AddWithValue("@name", name);
                test_available.Parameters.AddWithValue("@ean", ean);
                SqliteDataReader avail_reader = test_available.ExecuteReader();

                while (avail_reader.Read())
                {
                    name_availiable = avail_reader.GetString(0);
                    unit_availiable = avail_reader.GetString(1);
                }

                // if - Produkt ist vorhanden => Nur die Menge addieren
                if (name_availiable != null && unit_availiable == unit)
                {
                    SqliteCommand update_menge = new("UPDATE ingredients SET quantity = quantity + @menge_neu WHERE name = @name", conn);
                    update_menge.Parameters.AddWithValue("@menge_neu", quantity);
                    update_menge.Parameters.AddWithValue("@name", name);
                    await update_menge.ExecuteNonQueryAsync();
                    already_added = true;
                }
            }

            if (!already_added)
            {
                // Lebensmittel hinzufügen
                SqliteCommand ing_insert = new("INSERT INTO ingredients (name, quantity, unit) VALUES (@name, @quantity, @unit)", conn);
                ing_insert.Parameters.AddWithValue("@name", name);
                ing_insert.Parameters.AddWithValue("@quantity", Math.Round(quantity, 2));
                ing_insert.Parameters.AddWithValue("@unit", unit);
                await ing_insert.ExecuteNonQueryAsync();

                // max Ingredient_ID holen => aktuell hinzugefügte Zutat
                SqliteCommand ing_id_select = new("SELECT MAX(id) from ingredients", conn);
                SqliteDataReader reader = await ing_id_select.ExecuteReaderAsync();

                if (reader.Read()) { ingredient_id = reader.GetInt32(0); }

                if (ingredient_id != null && !string.IsNullOrWhiteSpace(ean))
                {
                    // EAN-Code in die DB schreiben
                    SqliteCommand ean_insert = new("INSERT INTO eancodes (eancode, name) VALUES (@eancode, @name)", conn);
                    ean_insert.Parameters.AddWithValue("@eancode", ean);
                    ean_insert.Parameters.AddWithValue("@name", name);
                    await ean_insert.ExecuteNonQueryAsync();
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Produktname anhand der EAN holen
    public static string RetrieveProductNameByEAN(SqliteConnection conn, string ean)
    {
        // Variablen
        string ean_code = string.Empty;
        string name = string.Empty;

        try
        {
            // Prüfen ob die EAN vorhanden ist in der lokalen Datenbank vorhanden ist
            SqliteCommand select_ean = new("SELECT name FROM eancodes WHERE eancode = @eancode", conn);
            select_ean.Parameters.AddWithValue("@eancode", ean);
            SqliteDataReader reader = select_ean.ExecuteReader();

            // while - Reader liesst Daten
            while (reader.Read())
            {
                name = reader.GetString(0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return name;
    }

    // benutzte Produkte aus der Datenbank für den Bestand entfernen
    public static void DeleteIngredient(Ingredients[] ingredients, SqliteConnection conn)
    {
        foreach (Ingredients ingredient in ingredients)
        {
            // Zuerst alle abhängigen Einträge in recipeingredients löschen
            //SqliteCommand deleteRecipeIngredients = new("DELETE FROM recipeingredients WHERE ingredient_id = @ingredientId", conn);
            //deleteRecipeIngredients.Parameters.AddWithValue("@ingredientId", ingredient.Id);
            //deleteRecipeIngredients.ExecuteNonQuery();

            SqliteCommand cmd = new("DELETE FROM ingredients where id = @id", conn);
            cmd.Parameters.AddWithValue("@id", ingredient.Id);
            cmd.ExecuteNonQuery();
        }
    }
}