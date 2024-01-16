using Microsoft.Data.Sqlite;
using Rezeptify.AppComponents.Models;
using Rezeptify.AppComponents;

namespace Rezeptify.AppComponents
{
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
                if (!File.Exists(databasePath)) {File.Create(databasePath);}
                
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

                SqliteCommand create_recipeingredients = conn.CreateCommand();
                create_recipeingredients.CommandText = "CREATE TABLE IF NOT EXISTS recipeingredients (recipe_id INTEGER NOT NULL, ingredient_id INTEGER NOT NULL, CONSTRAINT '0' FOREIGN KEY (ingredient_id) REFERENCES ingredients (id) ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT '1' FOREIGN KEY (recipe_id) REFERENCES recipe (id) ON UPDATE NO ACTION ON DELETE NO ACTION);";
                create_recipeingredients.ExecuteNonQuery();

                SqliteCommand create_eancodes = conn.CreateCommand();
                create_eancodes.CommandText = "CREATE TABLE IF NOT EXISTS eancodes ( id INTEGER NOT NULL, eancode VARCHAR(255) NOT NULL, ingredient_id INTEGER NOT NULL, PRIMARY KEY (id), CONSTRAINT '0' FOREIGN KEY (ingredient_id) REFERENCES ingredients (id) ON UPDATE NO ACTION ON DELETE NO ACTION);";
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
                SqliteCommand loadIng_command = new("SELECT * from ingredients", conn);
                SqliteDataReader reader = loadIng_command.ExecuteReader();

                // while - Daten werden gelesen
                while (reader.Read())
                {
                    // temporäre Ingredient erstellen und Daten lesen
                    Ingredients temp = new()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetFloat(3),
                        Unit = reader.GetString(2)
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
        public static void AddIngredients(string name, float quantity, string unit, string ean, SqliteConnection conn)
        {
            // Variablen
            int? ingredient_id = null;
            
            try
            {
                // Lebensmittel hinzufügen
                SqliteCommand ing_insert = new("INSERT INTO ingredients (name, quantity, unit) VALUES (@name, @quantity, @unit)",conn);
                ing_insert.Parameters.AddWithValue("@name", name);
                ing_insert.Parameters.AddWithValue("@quantity", quantity);
                ing_insert.Parameters.AddWithValue("@unit", unit);
                ing_insert.ExecuteNonQuery();

                // max Ingredient_ID holen => aktuell hinzugefügte Zutat
                SqliteCommand ing_id_select = new("SELECT MAX(id) from ingredients",conn);
                SqliteDataReader reader = ing_id_select.ExecuteReader();

                if (reader.Read()) { ingredient_id = reader.GetInt32(0); }

                if (ingredient_id != null && !String.IsNullOrWhiteSpace(ean))
                {
                    // EAN-Code in die DB schreiben
                    SqliteCommand ean_insert = new("INSERT INTO eancodes (eancode, ingredient_id) VALUES (@eancode, @ingredient_id)");
                    ean_insert.Parameters.AddWithValue("@eancode", ean);
                    ean_insert.Parameters.AddWithValue("@ingredient_id", ingredient_id);
                    ean_insert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
