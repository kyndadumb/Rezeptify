namespace Rezeptify.VM;

public interface IViewManager
{
    public void Show(object vm,bool hasAnimation = true);
    public Task ShowPopUp(object vm);
    public Task HandleErrorAsync(Exception ex);
    public Task MessageBoxAsync(string title, string msg);
    public Task MessageBoxAsyncCustom(string title, string msg, string btnText);
    public Task<bool> MessageBoxAsyncYesNo(string title, string msg);
    public Task<bool> MessageBoxAsyncYesNoCustom(string title, string msg, string yes, string no);

}
