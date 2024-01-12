using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class ActionCommand : CommandBase
{
    private Func<Task> _action;

    public ActionCommand(Func<Task> action)
    {
        _action = action;
    }

    public override void Execute(object? parameter)
    {
        if (_action == null || IsAllowed == false) return;
        _action.Invoke();
        base.Execute(parameter);
    }
}
