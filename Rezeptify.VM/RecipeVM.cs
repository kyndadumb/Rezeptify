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

        private void ShowStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm,false);
        }

        public ActionCommand CMD_StartPage { get; set; }
    }
}
