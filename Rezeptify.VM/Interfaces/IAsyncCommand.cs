using System.Windows.Input;

namespace Rezeptify.VM;

internal interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object par = null);
    bool CanExecute();
}
