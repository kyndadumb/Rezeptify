using Rezeptify.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify;

public class VMResolver
{
    public static Type Resolve(ViewModelBase vm)
    {
        if (vm.GetType() == typeof(StartVM)) return typeof(StartPage);
        throw new Exception("Keine Page für dieses VM registriert!");
    }
}
