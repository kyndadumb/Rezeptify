using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rezeptify.VM.Interfaces
{
    internal interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object par = null);
        bool CanExecute();
    }
}
