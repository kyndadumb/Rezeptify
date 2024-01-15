using Rezeptify.AppComponents.Models;
using System.Collections.ObjectModel;

namespace Rezeptify.VM
{
    public class RecipeVM : ViewModelBase
    {
        public RecipeVM()
        {
            this.CMD_StartPage = new ActionCommand(ShowStartPage);
            this.CMD_Result = new ActionCommand(ShowRecipeResultPage);
            TestCollection();
        }

        private async void ShowRecipeResultPage()
        {
            var vm = new BarcodeVM(this);
            _viewManager.Show(vm);
        }

        private void ShowStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm,false);
        }
        public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];

        private void TestCollection()
        {
            Ingredients ingredient1 = new Ingredients();
            ingredient1.Quantity = 1;
            ingredient1.Name = "Test";
            IngredientsCollection.Add(ingredient1);
            Ingredients ingredient2 = new Ingredients();
            ingredient2.Quantity = 1;
            ingredient2.Name = "SuperultramegalangertestversionEins";
            IngredientsCollection.Add(ingredient2);
            Ingredients ingredient3 = new Ingredients();
            ingredient3.Quantity = 1;
            ingredient3.Name = "Test";
            IngredientsCollection.Add(ingredient3);
        }

        public ActionCommand CMD_StartPage { get; set; }

        public ActionCommand CMD_Result { get; set; }
    }
}
