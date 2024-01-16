using Microsoft.Data.Sqlite;
using Rezeptify.AppComponents;
using Rezeptify.AppComponents.Models;
using System.Collections.ObjectModel;

namespace Rezeptify.VM;

public class StartVM : ViewModelBase
{
    public StartVM()
    {
        this.CMD_ShowTest = new ActionCommand(ShowTestPage);
        this.CMD_ShowRecipe = new ActionCommand(ShowRecipePage);
        this.CMD_ShowPopUp = new ActionCommand(ShowAddFoodPopUpPage);
        this.CMD_Help = new ActionCommand(ShowHelpPage);
    }

    public override Task OnShow()
    {
        LoadIngredients();
        return base.OnShow();
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

    private void ShowTestPage()
    {
        var vm = new TestPageVM(this);
        this._viewManager.Show(vm);
    }

    private void ShowAddFoodPopUpPage()
    {
        var vm = new AddFoodPopUpVM();
        _viewManager.ShowPopUp(vm);
    }

    private void ShowRecipePage()
    {
        var vm = new RecipeVM();
        _viewManager.Show(vm, false);
    }

    private void ShowHelpPage()
    {
        var vm = new HelpVM(this);
        _viewManager.Show(vm, false);
    }

    public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];


    public ActionCommand CMD_ShowTest { get; set; }

    public ActionCommand CMD_ShowBarcode { get; set; }

    public ActionCommand CMD_ShowRecipe { get; set; }

    public ActionCommand CMD_ShowPopUp { get; set; }

    public ActionCommand CMD_Help { get; set; }
}
