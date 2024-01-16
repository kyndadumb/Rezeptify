using Rezeptify.AppComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class BarcodeVM : ViewModelBase
{
    private ViewModelBase _backVM;
    private bool IsScanAllowed = false;
    public BarcodeVM(ViewModelBase backVM)
    {
        _backVM = backVM;
        CMD_Back = new ActionCommand(GoBack);
        CMD_BarcodeScanned = new TaskCommandWithPar(BarcodeScanned);
        CMD_ToggleFlashlight = new ActionCommand(ToggleFlashlight);
        IsScanAllowed = true;
    }

    private void ToggleFlashlight()
    {
        TorchEnabled = !TorchEnabled;
    }

    private async Task BarcodeScanned(object arg)
    {
        if (IsScanAllowed == false) return;
        ErrorText = "";
        try
        {
            // gucken ob Code valide ist
            var scannedCode = (string)arg ?? "";
            if (String.IsNullOrWhiteSpace(scannedCode)) return;
            IsScanAllowed = false;
            ErrorText = scannedCode;

            //Produktkategorie für Code raussuchen
            string cat = SearchLocalDBforEAN(scannedCode); //in der lokalen Datenbank nach Code gucken
            if (String.IsNullOrWhiteSpace(cat)) 
            {
                cat = await SearchOpenGTIN(scannedCode); //in OpenGTIN nach Code suchen
            }


            //zur Mengeneingabe
            var vm = new AddFoodVM(_backVM, cat, scannedCode);
            _viewManager.Show(vm);
        }
        catch (Exception ex)
        {

            ErrorText = ex.Message;
        }
    }

    private static string SearchLocalDBforEAN(string scannedCode)
    {
        using (var conn = DatabaseHandler.OpenDatabaseConnection())
        {
            return DatabaseHandler.RetrieveProductNameByEAN(conn, scannedCode);
        }
    }

    private static async Task<string> SearchOpenGTIN(string scannedCode)
    {
        var gtinHandler = new OpenGTINDBHandler("400000000");
        var gtinInfo = await gtinHandler.GetProductInformation(scannedCode);
        var cat = gtinHandler.ReturnInfoByCategory(gtinInfo, "name");
        return cat;
    }

    private void GoBack()
    {
        _viewManager.Show(_backVM);
    }
    private bool _TorchEnabled;

    public bool TorchEnabled
    {
        get { return _TorchEnabled; }
        set { _TorchEnabled = value; NotifyPropertyChanged(); }
    }


    public ActionCommand CMD_Back { get; set; }

    public TaskCommandWithPar CMD_BarcodeScanned { get; set; }
    public ActionCommand CMD_ToggleFlashlight { get; set; }

    private string _ErrorText;

    public string ErrorText
    {
        get { return _ErrorText; }
        set { _ErrorText = value; NotifyPropertyChanged(); }
    }

}
