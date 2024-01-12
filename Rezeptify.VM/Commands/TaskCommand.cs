using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

public class TaskCommand : CommandBase
{
    public TaskCommand(Func<object, Task> function)
    {
        _function = function;
    }

    //public TaskCommand(Action<Task> function)
    //{
    //    _function = function;
    //}

    private Func<object, Task> _function;

    public override void Execute(object? parameter)
    {
        if (_function == null || IsAllowed == false) return;
        _function.Invoke(parameter);
        base.Execute(parameter);
    }
}
