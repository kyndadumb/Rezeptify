using Rezeptify.AppComponents;
using Rezeptify.VM;
using Components = Rezeptify.AppComponents.Components;

namespace Rezeptify
{
    public partial class EntryPoint : ContentPage
    {

        public EntryPoint()
        {
            try
            {
                InitializeComponent();
                LoadComponents();
                CheckLocalFilesDirectory();
                var vm = new StartVM();
                var viewManager = Components.GetService<IViewManager>();
                viewManager.Show(vm);
            }
            catch (Exception ex)
            {
                this.ErrorLbl.Text = ex.Message + Environment.NewLine + (ex.InnerException?.Message ?? "");
            }
        }

        private void LoadComponents()
        {
            Components.RegisterService(typeof(IFileManager),new FileManager()); 

            var dgt = new ViewModelResolver(VMResolver.Resolve);
            var shell = (Shell)App.Current!.MainPage;
            var viewManager = new ViewManager(dgt, shell);
            Components.RegisterService(typeof(IViewManager),viewManager);
        }

        private static void CheckLocalFilesDirectory()
        {
            FileManager? service = Components.GetService<FileManager>();
            string appdata_path = Path.Combine(service.GetAppDataDir(), "Rezeptify");

            if (!Directory.Exists(appdata_path))
            {
                Directory.CreateDirectory(appdata_path);
            }
        }
    }
}
