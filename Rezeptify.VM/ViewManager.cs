using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
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
            //hier kann ein OnLeave Event vom ViewModel verlinkt werden
            await Task.Delay(0);
        }
    }

    private async Task EnterPage(Page pageToEnter)
    {
        ViewModelBase vm;
        if (pageToEnter != null)
        {
            //hier kann ein OnShow Event vom ViewModel verlinkt werden
            await Task.Delay(0);
        }
    }

    public async void Show(object vm, bool hasAnimation = true)
    {
        var targetIndex = FindBackStackIndex((ViewModelBase)vm, _rootPage); //Ist die Seite die geöffnet werden soll schon im NavigationStack?
        if (targetIndex == -1) //Nein -> Seite neu öffnen
        {
            var type = _Resolve((ViewModelBase)vm);
            if (type == null) throw new Exception($"Kein View für {vm.GetType().ToString} gefunden");

            await NavToPage(type, vm, hasAnimation);
        }
        else  //ja -> Zu der Seite zurücknavigieren
        {
            do
            {
                var pages = _rootPage.Navigation.NavigationStack;
                var pagesBackStack = pages.ToList();
                pagesBackStack.RemoveAt(pagesBackStack.Count - 1);
                var maxIndex = pagesBackStack.Count() - 1;

                if (maxIndex > targetIndex)
                {
                    await _rootPage.Navigation.PopAsync(false); //Zielseite noch nicht erreicht, weiter zurücknavigieren
                }
                else
                {
                    await _rootPage.Navigation.PopAsync(hasAnimation); //Zielseite wird erreicht, Animation anzeigen und raus aus Loop!
                    return;
                }
            } while (true);
        }
    }

    public async Task ShowPopUp(object vm)
    {
        var type = _Resolve((ViewModelBase)vm);
        if (type == null) throw new Exception($"Kein Popup für {vm.GetType().ToString()} gefunden");
        var popupInstance = (Popup)System.Activator.CreateInstance(type)!;
        popupInstance.BindingContext = (ViewModelBase)vm;

        await _rootPage.ShowPopupAsync(popupInstance);
    }

    private static int FindBackStackIndex(ViewModelBase vm, Shell shell)
    {
        var pages = shell.Navigation.NavigationStack;
        for (int i = pages.Count() - 1; i >= 0; i--)
        {
            var page = pages[i];
            if (page == null) continue;
            if (page.BindingContext == vm) return i;
        }
        return -1;
    }

    private async Task NavToPage(Type type, object vm, bool hasAnimation)
    {
        var pageInstance = (Page)System.Activator.CreateInstance(type)!;
        pageInstance.BindingContext = vm;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _rootPage.Navigation.PushAsync(pageInstance, hasAnimation);
        });
    }

    public async Task HandleErrorAsync(Exception ex)
    {
        await MessageBoxAsync("Fehler", ex.Message);
    }

    public async Task MessageBoxAsync(string title, string msg)
    {
        await _rootPage.DisplayAlert(title, msg, "OK");
    }
    public async Task MessageBoxAsyncCustom(string title, string msg, string btnText)
    {
        await _rootPage.DisplayAlert(title, msg, btnText);
    }
    public async Task MessageBoxAsyncYesNo(string title, string msg)
    {
        await _rootPage.DisplayAlert(title, msg, "Ja", "Nein");
    }
    public async Task MessageBoxAsyncYesNoCustom(string title, string msg, string yes, string no)
    {
        await _rootPage.DisplayAlert(title, msg, yes, no);
    }
}
