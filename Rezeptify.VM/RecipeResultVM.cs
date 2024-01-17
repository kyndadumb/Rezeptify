﻿using DeepL;
using Rezeptify.AppComponents;
using Rezeptify.AppComponents.Models;

namespace Rezeptify.VM
{
    public class RecipeResultVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        private Ingredients[] _ingredients = [];
        private int _portionen;
        public RecipeResultVM(ViewModelBase backVM, Ingredients[] ingredients, int portions = 1)
        {
            _ingredients = ingredients;
            _backVM = backVM;
            _portionen = portions;
            CMD_Back = new TaskCommand(BackToRecipe);
        }

        public override async Task OnShow()
        {
            await CreateRecipe();
            await base.OnShow();
        }

        private async Task BackToRecipe()
        {
            var shouldClose = await _viewManager.MessageBoxAsyncYesNo("Rezept Schließen?", "Soll das Rezept wirklich geschlossen werden?");
            if (!shouldClose) return;
            var removeUsedIngredients = await _viewManager.MessageBoxAsyncYesNo("", "Sollen die benutzen Zutaten von der Zutatenliste entfernt werden?");
            if (removeUsedIngredients)
            {
                //Zutaten entfernen
                using (var conn = DatabaseHandler.OpenDatabaseConnection())
                {
                    DatabaseHandler.DeleteIngredient(_ingredients, conn);
                }
            }
            _viewManager.Show(_backVM);
        }

        private async Task CreateRecipe()
        {
            InstructionsText = "";
            try
            {
                //ChefGPTHandler chefGPTHandler = new();
                //Translator translator = new("ec61c033-fbcc-4e92-d7ac-cc39ca3cf507:fx");
                //RecipeRequest recipe_request = await chefGPTHandler.CreateRecipeRequest(_ingredients, null, null, null, null, null, translator, "Metric");
                //string recipe = await chefGPTHandler.RequestRecipe(recipe_request);
                //string instructions = chefGPTHandler.ExtractInstructionSet(recipe);
                //string instructions_deutsch = await DeepLHandler.TranslateInstructions(translator, instructions, "DE-DE");
                //InstructionsText = instructions_deutsch;
            }
            catch (Exception ex)
            {
                InstructionsText = ex.Message;
            }
        }

        private string _InstructionsText;

        public string InstructionsText
        {
            get { return _InstructionsText; }
            set { _InstructionsText = value; NotifyPropertyChanged(); }
        }

        public TaskCommand CMD_Back { get; set; }
    }
}
