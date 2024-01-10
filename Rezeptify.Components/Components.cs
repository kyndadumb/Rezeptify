using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.AppComponents;

public static class Components
{

    private static Dictionary<Type, object> Services = [];

    public static void RegisterService(Type service, object instance)
    {
        Services.Add(service, RuntimeHelpers.GetObjectValue(instance));
    }

    public static T GetService<T>()
    {
        if (Services.ContainsKey(typeof(T)))
        {
            return Conversions.ToGenericParameter<T>(Services[typeof(T)]);
        }
        throw new Exception($"Kein registrierten Service für {typeof(T)} gefunden!");
    }

}
