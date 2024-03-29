﻿namespace Rezeptify.AppComponents.Models
{
    public class RecipeRequest
    {
        public List<string> Ingredients { get; set; } = new();
        public string? MealType { get; set; }
        public List<string>? KitchenTools { get; set; } = new();
        public int? PreparationTime { get; set; }
        public int? Servings { get; set; }
        public string? Difficulty { get; set; }

        public string Measurement { get; set; }
    }
}
