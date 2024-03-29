﻿using Rezeptify;
using Rezeptify.VM;

namespace Rezeptify;

public class VMResolver
{
    public static Type Resolve(ViewModelBase vm)
    {
        if (vm.GetType() == typeof(StartVM)) return typeof(StartPage);
        if (vm.GetType() == typeof(TestPageVM)) return typeof(TestPage);
        if (vm.GetType() == typeof(BarcodeVM)) return typeof(BarcodePage);
        if (vm.GetType() == typeof(RecipeVM)) return typeof(RecipePage);
        if (vm.GetType() == typeof(RecipeResultVM)) return typeof(RecipeResultPage);
        if (vm.GetType() == typeof(AddFoodVM)) return typeof(AddFoodPage);
        if (vm.GetType() == typeof(AddFoodPopUpVM)) return typeof(AddFoodPopUpPage);
        if (vm.GetType() == typeof(HelpVM)) return typeof(HelpPage);
        throw new Exception("Keine Page für dieses VM registriert!");
    }
}
