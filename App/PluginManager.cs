namespace PluginApp.App;

using System.Reflection;
using PluginApp.PluginBase;

public static class PluginManager
{
    public static (PluginLoadContext, Assembly) LoadPlugin(string path)
    {
        string location = Path.GetFullPath(path);
        PluginLoadContext context = new(location);
        Assembly assembly = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(location)));
        return (context, assembly);
    }
    public static void LoadPlugins(string pluginPath, Action<Assembly, PluginLoadContext, IPlugin?>? Callback = null)
    {
        throw new NotImplementedException();
    }
    public static IPlugin? GetPluginClass(Assembly assembly)
    {
        var types = from t in assembly.GetTypes()
                    where t.GetInterfaces().Contains(typeof(IPlugin))
                    select t;
        if (types is null || types.Count() != 1)
            return null;
        return Activator.CreateInstance(types.First()) as IPlugin;
    }
}
