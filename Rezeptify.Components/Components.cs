using System.Runtime.CompilerServices;

namespace Rezeptify.AppComponents;

public static class Components
{
    //Collection für alle Services
    private static Dictionary<Type, object> Services = [];

    //Service registrieren
    public static void RegisterService(Type service, object instance)
    {
        Services.Add(service, RuntimeHelpers.GetObjectValue(instance));
    }

    //Service holen
    public static T GetService<T>()
    {
        if (Services.ContainsKey(typeof(T)))
        {
            //return Conversions.ToGenericParameter<T>(Services[typeof(T)]);
            return (T)(Services[typeof(T)]);
        }
        throw new Exception($"Kein registrierten Service für {typeof(T)} gefunden!");
    }

}
