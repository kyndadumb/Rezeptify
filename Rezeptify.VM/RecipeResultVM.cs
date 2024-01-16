using Rezeptify.AppComponents;
using Rezeptify.AppComponents.Models;

namespace Rezeptify.VM
{
    public class RecipeResultVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        private Ingredients[] _ingredients = [];
        public RecipeResultVM(ViewModelBase backVM, Ingredients[] ingredients)
        {
            _ingredients = ingredients;
            _backVM = backVM;
            CMD_Back = new ActionCommand(BackToRecipe);
        }

        public override async Task OnShow()
        {
            await ChefGPTHandler.RequestRecipe();
            await base.OnShow();
        }

        private void BackToRecipe()
        {
            _viewManager.Show(_backVM);
        }

        public ActionCommand CMD_Back { get; set; } 
    }
}
