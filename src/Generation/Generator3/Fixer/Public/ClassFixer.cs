using Generator3.Converter;

namespace Generator3.Fixer.Public;

public static class ClassFixer
{
    public static void Fixup(this GirModel.Class @class)
    {
        FixPublicMethodsColldingWithProperties(@class);
        FixInternalMethodsNamedLikeClass(@class);
    }

    private static void FixPublicMethodsColldingWithProperties(GirModel.Class @class)
    {
        foreach (var property in @class.Properties)
        {
            foreach (var method in @class.Methods)
            {
                if (property.GetPublicName() == method.GetPublicName())
                {
                    var newName = $"Get{method.GetPublicName()}";
                    Log.Warning($"{@class.Namespace.Name}.{@class.Name}: Property {property.GetPublicName()} collides with method {method.GetPublicName()} and is renamed to {newName}.");

                    method.SetPublicName(newName);
                }
            }
        }
    }

    private static void FixInternalMethodsNamedLikeClass(GirModel.Class @class)
    {
        foreach (var method in @class.Methods)
        {
            if (method.Name == @class.Name)
            {
                Log.Warning($"{@class.Name}: Method {method.Name} is named like its containing class. This is not allowed. The method should be created with a suffix and be rewritten to it's original name");
                method.SetInternalName(method.Name + "1");
            }   
        }
    }
}
