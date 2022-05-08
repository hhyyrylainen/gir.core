using System.Collections.Generic;
using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Enumerations
{
    public static void Generate(this IEnumerable<GirModel.Enumeration> enumerations, string path)
    {
        var publisher = new FilePublisher(path);
        var generator = new PublicEnumerationGenerator();

        foreach (var enumeration in enumerations)
        {
            var codeUnit = generator.Generate(enumeration);
            publisher.Publish(codeUnit);
        }
    }
}
