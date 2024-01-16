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
        //this.CMD_ShowBarcode = new ActionCommand(ShowScanPage);
        this.CMD_ShowRecipe = new ActionCommand(ShowRecipePage);
        this.CMD_ShowPopUp = new ActionCommand(ShowAddFoodPopUpPage);
        this.CMD_Help = new ActionCommand(ShowHelpPage);
        //TestCollection();

        LoadIngredients();
    }

    private void TestCollection()
    {
        Ingredients ingredient1 = new Ingredients();
        ingredient1.Quantity = 1;
        ingredient1.Name = "Test";
        IngredientsCollection.Add(ingredient1);
        Ingredients ingredient2 = new Ingredients();
        ingredient2.Quantity = 1;
        ingredient2.Name = "Superultramegalangertest";
        IngredientsCollection.Add(ingredient2);
        Ingredients ingredient3 = new Ingredients();
        ingredient3.Quantity = 1;
        ingredient3.Name = "Test";
        IngredientsCollection.Add(ingredient3);
    }

    private void LoadIngredients()
    {
        SqliteConnection sqliteConnection = DatabaseHandler.OpenDatabaseConnection();
        List<Ingredients> list = DatabaseHandler.LoadIngredients(sqliteConnection);

        foreach (Ingredients ing in list) { IngredientsCollection.Add(ing); }
    }

    private void ShowScanPage()
    {
        var vm = new BarcodeVM(this);
        _viewManager.Show(vm);
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
