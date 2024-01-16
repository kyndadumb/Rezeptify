using Rezeptify.AppComponents.Models;
using Rezeptify.AppComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class RecipeResultVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        public RecipeResultVM(ViewModelBase backVM)
        {
            _backVM = backVM;
            CMD_Back = new ActionCommand(BackToRecipe);
        }

        private void BackToRecipe()
        {
            _viewManager.Show(_backVM);
        }

        private async Task CreateRecipe(Ingredients[] selected_ingredients)
        {
            ChefGPTHandler chefGPTHandler = new();
            RecipeRequest recipe_request = chefGPTHandler.CreateRecipieRequest(selected_ingredients, null, null, null, null, null);
            string recipe = await chefGPTHandler.RequestRecipe(recipe_request);
            string instructions = chefGPTHandler.ExtractInstructionSet(recipe);
        }

        public ActionCommand CMD_Back { get; set; } 
    }
}
