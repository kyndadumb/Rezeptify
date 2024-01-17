using ChatGPT;
using ChatGPT.Net;

namespace Rezeptify.AppComponents;

public class ChatGPTHandler
{
    public static ChatGpt CreateBot(string API_KEY)
    {
        ChatGpt bot = new(API_KEY);
        return bot;
    }

    public static async Task<string> AskForRecipe(ChatGpt bot, Ingredients[] ingredients, int portionen)
    {
        // Variablen
        string result = string.Empty;
        string text_ingredients = string.Empty;

        foreach (Ingredients ing in ingredients)
        {
            text_ingredients += ing.Quantity.ToString() + ing.Unit.ToString() + " " +  ing.Name.ToString() + " ";
        }
        
        string prompt = $"Bitte erstelle ein Rezept aus {text_ingredients.TrimEnd()}. " +
            $"Du musst nicht alle Zutaten nutzen, aber auch keine anderen Zutaten dazudichten." +
            $"Das Rezept soll für {portionen} Portionen reichen" +
            $"Gib nur einen Text mit den Instruktionen zum Kochen zurück";

        string response = await bot.Ask(prompt);

        // if - Antwort bekommen
        if (response != null)
        {
            return response;
        }

        // leere Antwort zurückgeben
        return result;
    }
}
