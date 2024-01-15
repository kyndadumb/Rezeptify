using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class RecipeResultVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        public RecipeResultVM(ViewModelBase backVM)
        {
            _backVM = backVM;
            CMD_Back = new ActionCommand(BackToRecipe);
        }

        private void BackToRecipe()
        {
            _viewManager.Show(_backVM);
        }

        public ActionCommand CMD_Back { get; set; } 
    }
}
