
namespace Rezeptify.VM
{
    public class HelpVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        public HelpVM(ViewModelBase vm) 
        {
            _backVM = vm;
            this.CMD_Back = new ActionCommand(ShowPreviousPage);
        }

        private void ShowPreviousPage()
        {
            _viewManager.Show(_backVM);
            Task.Delay(0);
        }

        public ActionCommand CMD_Back { get; set; }
    }
}
