
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
                var vm = new StartVM();
            }
            catch (Exception ex)
            {
                this.ErrorLbl.Text = ex.Message + Environment.NewLine + (ex.InnerException?.Message ?? "");
            }
        }

        private void LoadComponents()
        {
            //Components.RegisterService<IViewManager,new ViewManager()>();
        }
    }
}
