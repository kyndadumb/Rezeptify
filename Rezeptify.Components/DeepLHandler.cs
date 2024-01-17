using DeepL;

namespace Rezeptify.AppComponents
{
    public class DeepLHandler
    {
        // Text aus einer List<string> nehmen, übersetzen und in die Liste zurückschreiben
        public static async Task<List<string>> TranslateIngredient(Translator translator, List<string> ingredients, string targetLang)
        {
            // Variablen
            List<string> result = new();

            var translations = await translator.TranslateTextAsync(ingredients, null, targetLang);
            foreach (var translation in translations) { result.Add(translation.Text); }

            return result;
        }

        // Instructions übersetzen
        public static async Task<string> TranslateInstructions(Translator translator, string instructions, string targetLanguage)
        {
            var translation = await translator.TranslateTextAsync(instructions, null, targetLanguage);
            return translation.Text;
        }
    }
}
