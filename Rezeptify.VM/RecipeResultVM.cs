using ChatGPT.Net;
using DeepL;
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
            await CreateRecipe(_ingredients);
            await base.OnShow();
        }

        private void BackToRecipe()
        {
            _viewManager.Show(_backVM);
        }

        private async Task CreateRecipe(Ingredients[] selected_ingredients)
        {
            InstructionsText = "";
            try
            {
                ChatGpt chatgpt = ChatGPTHandler.CreateBot("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");
                InstructionsText = await ChatGPTHandler.AskForRecipe(chatgpt, selected_ingredients, );
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

        public ActionCommand CMD_Back { get; set; } 
    }
}
