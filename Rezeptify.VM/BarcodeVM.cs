using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class BarcodeVM : ViewModelBase
{
    private ViewModelBase _backVM;
    public BarcodeVM(ViewModelBase backVM)
    {
        _backVM = backVM;
        CMD_Back = new ActionCommand(GoBack);
        CMD_BarcodeScanned = new TaskCommand(BarcodeScanned);
    }

    private async Task BarcodeScanned(object arg)
    {
        await Task.Delay(0);
        BarcodeTest = (string)arg;
    }

    private async Task GoBack()
    {
        await Task.Delay(0);
        _viewManager.Show(_backVM);
    }

    public ActionCommand CMD_Back { get; set; }

    public TaskCommand CMD_BarcodeScanned { get; set; }

    private string _BarcodeTest;

    public string BarcodeTest
    {
        get { return _BarcodeTest; }
        set { _BarcodeTest = value; NotifyPropertyChanged(); }
    }

}
