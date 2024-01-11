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
        

    }

    private Task ShowTestPage(object arg)
    {
        var vm = new TestPageVM();
        this._viewManager.Show(vm);
        return Task.CompletedTask;
    }

    private TaskCommand _CMD_ShowTest;

	public TaskCommand CMD_ShowTest
	{
		get { return _CMD_ShowTest; }
		set { _CMD_ShowTest = value; }
	}

}
