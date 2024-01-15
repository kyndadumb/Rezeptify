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
        CMD_BarcodeScanned = new ActionCommand(BarcodeScanned);
        CMD_ToggleFlashlight = new ActionCommand(ToggleFlashlight);
    }

    private void ToggleFlashlight()
    {
        TorchEnabled = !TorchEnabled;
    }

    private void BarcodeScanned(object arg)
    {
        BarcodeTest = (string)arg;
    }

    private void GoBack()
    {
        _viewManager.Show(_backVM);
    }
    private bool _TorchEnabled;

    public bool TorchEnabled
    {
        get { return _TorchEnabled; }
        set { _TorchEnabled = value; }
    }


    public ActionCommand CMD_Back { get; set; }

    public ActionCommand CMD_BarcodeScanned { get; set; }
    public ActionCommand CMD_ToggleFlashlight { get; set; }

    private string _BarcodeTest;

    public string BarcodeTest
    {
        get { return _BarcodeTest; }
        set { _BarcodeTest = value; NotifyPropertyChanged(); }
    }

}
