namespace Rezeptify
{
    internal class OpenGTINDBHandler
    {
        // Variablen
        private string? USER_ID;
        private readonly string baseURL = "http://opengtindb.org/";
        private readonly HttpClient httpClient;

        public OpenGTINDBHandler(string user_id)
        {
            SetUserID(user_id);
            httpClient = new HttpClient();
        }

        // API-Key setzen
        public void SetUserID(string user_id) => USER_ID = user_id;

        // API-Key holen
        public string? UserID => USER_ID;

        // Base-URL holen
        public string GetBaseURL() => baseURL;

        // Produktinformationen per HTTP-Abfrage holen
        public async Task<string> GetProductInformation(string gtin)
        {
            // Anfrage-URL definieren
            string url = $"{baseURL}?ean={gtin}&cmd=query&queryid={UserID}";

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

                // wurde Statuscode 200 geliefert?
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Antwort auslesen
                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }

                else { throw new Exception($"{httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase}"); }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler bei der Anfrage bei OpenGTINDB: {ex.Message}");
            }
        }

    }
}
