using System.Net.Http.Headers;

namespace Rezeptify.AppComponents
{
    internal class ChefGPTHandler
    {
        private string API_KEY = "24a52c5f-2007-4e4c-ba5d-d8d1cf93c0a2";
        private string BASE_URL = "https://api.chefgpt.com/";

        public async Task<string> RequestRecipe()
        {
            string result = null;

            HttpClient httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return result;
        }
    }
}
