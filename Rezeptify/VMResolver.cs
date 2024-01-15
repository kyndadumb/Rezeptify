using Rezeptify.Pages;
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
        if (vm.GetType() == typeof(TestPageVM)) return typeof(TestPage);
        if (vm.GetType() == typeof(BarcodeVM)) return typeof(BarcodePage);
        if (vm.GetType() == typeof(RecipeVM)) return typeof(RecipePage);
        if(vm.GetType() == typeof(RecipeResultVM)) return typeof (RecipeResultPage);
        throw new Exception("Keine Page für dieses VM registriert!");
    }
}
