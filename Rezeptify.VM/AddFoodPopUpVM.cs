using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class AddFoodPopUpVM : ViewModelBase
    {
        public AddFoodPopUpVM() {
            this.CMD_ShowBarcode = new ActionCommand(ShowScanPage);
            this.CMD_ShowAddFood = new ActionCommand(ShowAddFoodPage);
        }

        private void ShowAddFoodPage()
        {
            var vm = new AddFoodVM(new StartVM());
            _viewManager.Show(vm);
        }

        private void ShowScanPage()
        {
            var vm = new BarcodeVM(new StartVM());
            _viewManager.Show(vm);
        }

        public ActionCommand CMD_ShowBarcode { get; set; }
        public ActionCommand CMD_ShowAddFood {  get; set; }
    }
}
