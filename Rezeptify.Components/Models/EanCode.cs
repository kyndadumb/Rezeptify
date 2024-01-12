namespace Rezeptify.AppComponents.Models
{
    internal class EanCode
    {
        public int Id { get; set; }
        public string EanCodeValue { get; set; }
        public int IngredientId { get; set; }
    }
}
