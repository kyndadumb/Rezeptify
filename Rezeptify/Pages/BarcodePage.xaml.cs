using ZXing.Net.Maui;

namespace Rezeptify;

public partial class BarcodePage : ContentPage
{
    public BarcodePage()
    {
        InitializeComponent();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true,
        };
    }
}