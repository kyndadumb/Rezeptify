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

        public async Task<string> RequestRecipe()
        {
            string result = null;

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
