namespace PluginApp.App;

using System;
using System.Reflection;
using System.Runtime;
using System.Runtime.Loader;
using Newtonsoft.Json;

public class PluginLoadContext : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;
    public PluginLoadContext(string path)
    {
        _resolver = new AssemblyDependencyResolver(path);
    }
    protected override Assembly? Load(AssemblyName assemblyName)
    {
        string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath is not null)
            return LoadFromAssemblyPath(assemblyPath);
        return null;
    }
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string? unmanagedDllPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (unmanagedDllPath is not null)
            return LoadUnmanagedDllFromPath(unmanagedDllPath);
        return IntPtr.Zero;
    }
}

public class PluginInfo
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string License { get; set; }
    public string? Website { get; set; }

    // Avaliable values:
    // CLRPlugin
    // CLRLibrary
    // UnmanagedDll
    // UnmanagedDllLibrary
    public string PluginType { get; set; }
    public PluginInfo(string name, string version, string license, string pluginType)
    {
        Name = name;
        Version = version;
        License = license;
        PluginType = pluginType;
    }
}
