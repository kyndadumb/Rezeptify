using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class TestPageVM : ViewModelBase
{
    private ViewModelBase _backVM; 
    public TestPageVM(ViewModelBase vm)
    {
        _backVM = vm;
        CMD_Back = new TaskCommand(BackToStart);
    }

    private Task BackToStart(object arg)
    {
        _viewManager.Show(_backVM);
        return Task.CompletedTask;
    }

    public TaskCommand CMD_Back { get; set; }
}
