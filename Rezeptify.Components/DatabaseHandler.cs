using Microsoft.Data.Sqlite;
using Rezeptify.AppComponents.Models;

namespace Rezeptify.AppComponents
{
    class DatabaseHandler
    {
        static string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "rezeptify.db");

        // Datenbankverbindung eröffnen & Tabellen erstellen falls Sie nicht existieren
        public static void OpenDatabaseConnection()
        {
            try
            {
                SqliteConnection dbConn = new(connectionString: $"Data Source={databasePath}");
                dbConn.Open();

                // if - Datenbankconnection nicht eröffnet -> Fehler werfen
                if (dbConn.State != System.Data.ConnectionState.Open) throw new Exception("Verbindung zur Datenbank fehlgeschlagen!");

                // recipe-Tabelle erstellen falls sie nicht existiert
                SqliteCommand create_recipe = dbConn.CreateCommand();
                create_recipe.CommandText = "CREATE TABLE IF NOT EXISTS recipe (id INTEGER NOT NULL, instructions TEXT NULL, name TEXT NULL, PRIMARY KEY (id));";
                create_recipe.ExecuteNonQuery();

                SqliteCommand create_ingredients = dbConn.CreateCommand();
                create_ingredients.CommandText = "CREATE TABLE IF NOT EXISTS ingredients (id INTEGER NOT NULL, name TEXT NOT NULL, quantity REAL NULL, unit VARCHAR(50) NULL, PRIMARY KEY (id));";
                create_ingredients.ExecuteNonQuery();

                SqliteCommand create_recipeingredients = dbConn.CreateCommand();
                create_recipeingredients.CommandText = "CREATE TABLE IF NOT EXISTS recipeingredients (recipe_id INTEGER NOT NULL, ingredient_id INTEGER NOT NULL, CONSTRAINT 0 FOREIGN KEY (ingredient_id) REFERENCES ingredients (id) ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT 1 FOREIGN KEY (recipe_id) REFERENCES recipe (id) ON UPDATE NO ACTION ON DELETE NO ACTION);";
                create_recipeingredients.ExecuteNonQuery();

                SqliteCommand create_eancodes = dbConn.CreateCommand();
                create_eancodes.CommandText = "CREATE TABLE IF NOT EXISTS eancodes ( id INTEGER NOT NULL, eancode VARCHAR(255) NOT NULL, ingredient_id INTEGER NOT NULL, CONSTRAINT 0 FOREIGN KEY (ingredient_id) REFERENCES ingredients (id) ON UPDATE NO ACTION ON DELETE NO ACTION);";
                create_eancodes.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Vorhandene Zutaten aus der Datenbank laden
        public static List<Ingredients> LoadIngredients()
        {
            // Variablen
            List<Ingredients> ingredients = new();

            try
            {
                SqliteCommand loadIng_command = new("SELECT * from ingredients");
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
    }
}
