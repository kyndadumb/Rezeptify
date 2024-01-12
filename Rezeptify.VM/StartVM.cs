using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class StartVM : ViewModelBase
{
    public StartVM()
    {
        this.CMD_ShowTest = new TaskCommand(ShowTestPage);
        this.CMD_ShowBarcode = new ActionCommand(ShowScanPage);
        

    }

    private Task ShowScanPage()
    {
        var vm = new BarcodeVM(this);
        _viewManager.Show(vm);
        return Task.CompletedTask;
    }

    private Task ShowTestPage(object arg)
    {
        var vm = new TestPageVM(this);
        this._viewManager.Show(vm);
        return Task.CompletedTask;
    }

    private string _Penis;

    public string Penis
    {
        get { return _Penis; }
        set { _Penis = value; }
    }



    private TaskCommand _CMD_ShowTest;

	public TaskCommand CMD_ShowTest
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

}
