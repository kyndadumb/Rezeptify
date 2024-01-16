using Rezeptify.AppComponents.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Rezeptify.AppComponents
{
    public class ChefGPTHandler
    {
        private string API_KEY = "24a52c5f-2007-4e4c-ba5d-d8d1cf93c0a2";
        private string BASE_URL = "https://api.chefgpt.com/";
        private readonly HttpClient _httpClient;

        public ChefGPTHandler()
        {
            _httpClient = new HttpClient();
        }

        // RecipeRequest erstellen
        public RecipeRequest CreateRecipieRequest(Ingredients[] ingredients, string? mealtype, List<string> kitchen_tools, int? prep_time, int? servings, string? difficulty)
        {
            RecipeRequest result = new();

            foreach (Ingredients ing in ingredients) { result.Ingredients.Add(ing.Name.ToString() + " " + ing.Quantity + " " + ing.Unit); }
            if (mealtype != null) { result.MealType = mealtype; }
            if (kitchen_tools != null)
            {
                foreach (string tool in kitchen_tools) { result.KitchenTools.Add(tool); }
            }
            if (prep_time != null) { result.PreparationTime = prep_time; }
            if (servings != null) { result.Servings = servings; }
            if (difficulty != null) { result.Difficulty = difficulty; }

            return result;
        }

        // Rezept anfragen
        public async Task<string> RequestRecipe(RecipeRequest request)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(request);
            StringContent data = new(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);

            HttpResponseMessage response = await _httpClient.PostAsync(BASE_URL, data);

            // wurde Statuscode 200 geliefert ?
            if (response.IsSuccessStatusCode)
            {
                // Antwort auslesen
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        // Instructions aus dem Rückgabewert extrahieren
        public string ExtractInstructionSet(string anwser)
        {
            // Variablen
            string result = string.Empty;

            // anwser parsen & children in Liste speichern
            JObject anwser_jobject = JObject.Parse(anwser);
            IList<JToken> instructions = anwser_jobject["instructions"].Children().ToList();

            // if - keine Instructions gefunden => null zurückgeben
            if (instructions.Count == 0) { return null; }

            // foreach - alle Instructions in einem String speichern
            foreach (JToken instruction in instructions)
            {
                result += instruction.ToString() + "\n";
            }

            // Ergebnis zurückgeben
            return result;
        }
    }
}