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

            IngredientsCollection.Clear();
            foreach (Ingredients ing in list)
            {
                IngredientsCollection.Add(ing);
            }
        }

        private int _Portionen = 1;

        public int Portionen
        {
            get { return _Portionen; }
            set { _Portionen = value; NotifyPropertyChanged(); }
        }


        private void ShowRecipeResultPage()
        {
            Ingredients[]? ingredients = ConvertSelectedCollection();
            RecipeResultVM vm = new RecipeResultVM(this,ingredients,Portionen);
            _viewManager.Show(vm);
        }

        private Ingredients[] ConvertSelectedCollection()
        {
            List<Ingredients> list = [];
            foreach (object? ingr in SelectedIngredientCollection)
            {
                list.Add((Ingredients)ingr);
            }
            return list.ToArray();
        }

        private void ShowStartPage()
        {
            StartVM vm = new StartVM();
            _viewManager.Show(vm,false);
        }
        public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];
        public List<object> SelectedIngredientCollection { get; set; } = [];

        public ActionCommand CMD_StartPage { get; set; }

        public ActionCommand CMD_Result { get; set; }
    }
}
