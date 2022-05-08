using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Framework
{
    public static void Generate(this GirModel.Namespace ns, string path)
    {
        if (ns.Name == "GLib")
            return;//We can not register any type of GLib as GLib is not using the GObject type system
        
        var publisher = new FilePublisher(path);

        var extensionsGenerator = new InternalFrameworkExtensionsGenerator();
        var codeUnit = extensionsGenerator.Generate(ns);
        publisher.Publish(codeUnit);

        var typeRegistrationGenerator = new PublicFrameworkTypeRegistrationGenerator();
        var codeUnit2 = typeRegistrationGenerator.Generate(ns);
        publisher.Publish(codeUnit2);
    }
}
