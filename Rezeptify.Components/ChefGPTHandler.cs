using Rezeptify.AppComponents.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using DeepL;

namespace Rezeptify.AppComponents
{
    public class ChefGPTHandler
    {
        private string API_KEY = "97bc7d81-0fb3-4589-a1ec-432924716ad0";
        private string BASE_URL = "https://api.chefgpt.xyz/";
        private readonly HttpClient _httpClient;

        public ChefGPTHandler()
        {
            _httpClient = new HttpClient();
        }

        // RecipeRequest erstellen
        public async Task<RecipeRequest> CreateRecipeRequest(Ingredients[] ingredients, string? mealtype, List<string> kitchen_tools, int? prep_time, int? servings, string? difficulty, Translator translator, string measurement = "Metric")
        {
            RecipeRequest result = new();

            foreach (Ingredients ing in ingredients) { result.Ingredients.Add(ing.Name.ToString() + " " + ing.Quantity + " " + ing.Unit); }
            result.Ingredients = await DeepLHandler.TranslateIngredient(translator, result.Ingredients, "EN-US");
            
            if (mealtype != null) { result.MealType = mealtype; }
            if (kitchen_tools != null)
            {
                foreach (string tool in kitchen_tools) { result.KitchenTools.Add(tool); }
            }
            if (prep_time != null) { result.PreparationTime = prep_time; }
            if (servings != null) { result.Servings = servings; }
            if (difficulty != null) { result.Difficulty = difficulty; }

            result.Measurement = measurement;

            return result;
        }

        // Rezept anfragen
        public async Task<string> RequestRecipe(RecipeRequest request)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(request);
            StringContent data = new(json, Encoding.UTF8, "application/json");

            //_httpClient.DefaultRequestHeaders.Add("Authorization", API_KEY);

            string request_url = BASE_URL + "api/generate/recipe-from-ingredients";

            HttpRequestMessage requestMessage = new(HttpMethod.Post, request_url)
            {
                Content = data
            };

            requestMessage.Headers.Add("Authorization", API_KEY);

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            // wurde Statuscode 200 geliefert ?
            if (response.IsSuccessStatusCode)
            {
                // Antwort auslesen
                return await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync("OK");
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