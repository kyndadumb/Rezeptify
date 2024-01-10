using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Rezeptify.VM;

public class ViewManager : IViewManager
{
    private ViewModelResolver _Resolve;
    private Shell _rootPage;
    public ViewManager(ViewModelResolver resolver, Shell shell)
    {
        _Resolve = resolver;
        _rootPage = shell;

        Application.Current.PageAppearing += async (sender, e) =>
        {
            Exception exc;
            try
            {
                await EnterPage(e);
            }
            catch (Exception ex)
            {
                exc = ex;
            }
        };

        Application.Current.PageDisappearing += async (sender, e) =>
        {
            Exception exc;
            try
            {
                await LeavePage(e);
            }
            catch (Exception ex)
            {
                exc = ex;
            }
        };
    }

    private async Task LeavePage(Page pageToLeave)
    {
        ViewModelBase vm;
        if (pageToLeave != null)
        {
            await Task.Delay(0);
        }
    }

    private async Task EnterPage(Page pageToEnter)
    {
        ViewModelBase vm;
        if (pageToEnter != null)
        {
            await Task.Delay(0);
        }
    }

    public void Show(object vm)
    {
        var type = _Resolve((ViewModelBase)vm);
        if (type == null) throw new Exception($"Kein View für {vm.GetType().ToString} gefunden");

        NavToPage(type, vm);
    }

    private async Task NavToPage(Type? type, object vm)
    {
        await _rootPage.Navigation.PushAsync((Page)System.Activator.CreateInstance(type));
    }
}
