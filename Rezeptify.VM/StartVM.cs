using Rezeptify.VM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class StartVM : ViewModelBase
{
    public StartVM()
    {
        this.CMD_ShowTest = new ActionCommand(ShowTestPage);
        this.CMD_ShowBarcode = new ActionCommand(ShowScanPage);
        this.CMD_ShowRecipe = new ActionCommand(ShowRecipePage);
        TestCollection();

    }

    private void ShowRecipePage()
    {
        var vm = new RecipeVM();
        _viewManager.Show(vm,false);
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

    public ObservableCollection<Ingredients> IngredientsCollection { get; set; } = [];



    private ActionCommand _CMD_ShowTest;

	public ActionCommand CMD_ShowTest
	{
		get { return _CMD_ShowTest; }
		set { _CMD_ShowTest = value; }
	}

    private ActionCommand _CMD_ShowBarcode;

    public ActionCommand CMD_ShowBarcode
    {
        get { return _CMD_ShowBarcode; }
        set { _CMD_ShowBarcode = value; }
    }

    public ActionCommand CMD_ShowRecipe { get; set; }
}
