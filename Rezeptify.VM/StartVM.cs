﻿using Rezeptify.AppComponents.Models;
using System.Collections.ObjectModel;
using Rezeptify.AppComponents;
using Microsoft.Data.Sqlite;

namespace Rezeptify.VM;

public class StartVM : ViewModelBase
{
    public StartVM()
    {
        this.CMD_ShowTest = new ActionCommand(ShowTestPage);
        this.CMD_ShowRecipe = new ActionCommand(ShowRecipePage);
        this.CMD_ShowPopUp = new ActionCommand(ShowAddFoodPopUpPage);

        
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

        foreach (Ingredients ing in list) { IngredientsCollection.Add(ing); }
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

    public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];


    public ActionCommand CMD_ShowTest { get; set; }

    public ActionCommand CMD_ShowBarcode { get; set; }

    public ActionCommand CMD_ShowRecipe { get; set; }

    public ActionCommand CMD_ShowPopUp { get; set; }
}
