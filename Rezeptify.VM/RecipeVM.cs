using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class RecipeVM : ViewModelBase
    {
        public RecipeVM()
        {
            this.CMD_StartPage = new ActionCommand(ShowStartPage);
        }

        private async Task ShowStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm);
            await Task.Delay(0);
        }

        public ActionCommand CMD_StartPage { get; set; }
    }
}
