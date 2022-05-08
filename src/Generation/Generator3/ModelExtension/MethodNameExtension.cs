using System.Collections.Generic;

namespace Generator3.Converter;

public static class MethodNameExtension
{
    private static readonly Dictionary<GirModel.Method, string> FixedPublicNames = new();
    private static readonly Dictionary<GirModel.Method, string> FixedInternalNames = new();

    public static string GetInternalName(this GirModel.Method method)
    {
        return FixedInternalNames.TryGetValue(method, out var value)
            ? value
            : method.Name.ToPascalCase().EscapeIdentifier();
    }
    
    public static void SetInternalName(this GirModel.Method method, string name)
    {
        FixedPublicNames[method] = name;
    }

    public static string GetPublicName(this GirModel.Method method)
    {
        return FixedPublicNames.TryGetValue(method, out var value)
            ? value
            : method.Name.ToPascalCase().EscapeIdentifier();
    }

    public static void SetPublicName(this GirModel.Method method, string name)
    {
        FixedPublicNames[method] = name;
    }
}
