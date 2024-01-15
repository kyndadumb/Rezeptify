using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class ActionCommand : CommandBase
{
    private Action _action;
    private Action<object> _actionWithPar;

    public ActionCommand(Action action)
    {
        _action = action;
    }

    public ActionCommand(Action<object> action)
    {
        _actionWithPar = action;
    }

    public override void Execute(object? parameter)
    {
        if (IsAllowed == false) return;
        if (_action != null)
        {
            _action.Invoke();
        }
        else
        {
            _actionWithPar.Invoke(parameter!);
        }
    }
}
