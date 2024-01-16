using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Rezeptify.VM;

public class CommandBase : ICommand
{
    #region NotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    public void Notify([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _IsAllowed;
    }

    public virtual void Execute(object? parameter)
    {
    }

    public void RaiseCanExecuteChanged()
    {
        if (CanExecuteChanged == null) return;
        CanExecuteChanged(this,EventArgs.Empty);
    }

    private bool _IsAllowed = true;

    public bool IsAllowed
    {
        get { return _IsAllowed; }
        set
        {
            _IsAllowed = value;
            RaiseCanExecuteChanged();
            Notify();
        }
    }




}
