namespace Rezeptify.AppComponents;

public class OpenGTINDBHandler
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

    // Kategorie-Information aus der Produktinfo extrahieren
    public string ReturnInfoByCategory(string productinfo, string category)
    {
        string result = null;

        // Antwort anhand der Trennzeichen teilen
        string[] sections = productinfo.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);

        // Errorsection prüfen
        string errorSection = sections.Length > 0 ? sections[0] : string.Empty;

        // if - kein Fehler wurde gemeldet
        if (errorSection.Contains("error=0"))
        {
            // einzelne Sektionen nehmen und passendes Attribut liefern
            string productSection = sections.Length > 1 ? sections[1] : string.Empty;
            string[] attributes = productSection.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            result = SubstringKey(attributes, $"{category}=");
        }

        // Ergebnis zurückgeben, entweder Key oder leerer String
        return result;
    }

    // Key aus dem Input zurückgeben
    private string SubstringKey(string[] input, string key)
    {
        string result = string.Empty;
        
        foreach (string attribute in input)
        {
            if (attribute.StartsWith(key))
            {
                result =  attribute.Substring(key.Length);
            }
        }
        
        return result;
    }

}
