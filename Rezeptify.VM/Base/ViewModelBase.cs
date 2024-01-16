using System.ComponentModel;
using System.Runtime.CompilerServices;
using Components = Rezeptify.AppComponents.Components;

namespace Rezeptify.VM;

public class ViewModelBase : INotifyPropertyChanged
{
    #region NotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    } 
    #endregion

    internal readonly IViewManager _viewManager;

    public ViewModelBase()
    {
        _viewManager = Components.GetService<IViewManager>();
    }

    public virtual async Task OnShow()
    {
        await Task.Delay(0);
    }

    public virtual async Task OnLeave()
    {
        await Task.Delay(0);
    }
}
