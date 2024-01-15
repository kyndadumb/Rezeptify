
namespace Rezeptify.VM
{
    public class AddFoodVM : ViewModelBase
    {
        public AddFoodVM() 
        {
            this.CMD_ShowStart = new ActionCommand(showStartPage);
        }

        private void showStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm);
        }

        public ActionCommand CMD_ShowStart {  get; set; }
    }
}
