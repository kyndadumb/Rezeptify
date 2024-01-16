using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public interface IViewManager
{
    public void Show(object vm,bool hasAnimation = true);
    public Task ShowPopUp(object vm);
    public Task HandleErrorAsync(Exception ex);
    public Task MessageBoxAsync(string title, string msg);
    public Task MessageBoxAsyncCustom(string title, string msg, string btnText);
    public Task MessageBoxAsyncYesNo(string title, string msg);
    public Task MessageBoxAsyncYesNoCustom(string title, string msg, string yes, string no);

}
