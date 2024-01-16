using Microsoft.Data.Sqlite;
using Rezeptify.AppComponents;
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
            LoadIngredients();
        }

        private void LoadIngredients()
        {
            SqliteConnection sqliteConnection = DatabaseHandler.OpenDatabaseConnection();
            List<Ingredients> list = DatabaseHandler.LoadIngredients(sqliteConnection);

            foreach (Ingredients ing in list) { IngredientsCollection.Add(ing); }
        }

        private void ShowRecipeResultPage()
        {
            var ingredients = ConvertSelectedCollection();
            var vm = new RecipeResultVM(this);
            _viewManager.Show(vm);
        }

        private Ingredients[] ConvertSelectedCollection()
        {
            List<Ingredients> list = [];
            foreach (var ingr in SelectedIngredientCollection)
            {
                list.Add((Ingredients)ingr);
            }
            return list.ToArray();
        }

        private void ShowStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm,false);
        }
        public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];
        public List<object> SelectedIngredientCollection { get; set; } = [];

        public ActionCommand CMD_StartPage { get; set; }

        public ActionCommand CMD_Result { get; set; }
    }
}
