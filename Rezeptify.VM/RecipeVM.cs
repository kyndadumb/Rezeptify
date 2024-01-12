using Rezeptify.VM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class RecipeVM : ViewModelBase
    {
        public RecipeVM()
        {
            this.CMD_StartPage = new ActionCommand(ShowStartPage);
            TestCollection();
        }

        private async Task ShowStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm,false);
            await Task.Delay(0);
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
    }
}
