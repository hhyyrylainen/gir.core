using System.Collections.Generic;
using Generator3.V2.Controller;
using Generator3.V2.Generator.Class.Standard;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Interfaces
{
    public static void Generate(this IEnumerable<GirModel.Interface> interfaces, string path)
    {
        var publisher = new FilePublisher(path);
        
        foreach(var iface in interfaces)
            Generate(iface, publisher);
    }

    private static void Generate(GirModel.Interface iface, FilePublisher publisher)
    {
        var generators = new List<Generator<GirModel.Interface>>
        {
            new InternalInterfaceMethodsGenerator(),
            new PublicInterfaceFrameworkGenerator(),
            new PublicInterfaceMethodsGenerator(),
        };

        foreach (var generator in generators)
        {
            var codeUnit = generator.Generate(iface);
            publisher.Publish(codeUnit);
        }
    }
}
