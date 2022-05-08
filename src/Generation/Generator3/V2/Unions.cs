using System.Collections.Generic;
using System.Linq;
using Generator3.V2.Generator.Union;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Unions
{
    public static void Generate(this IEnumerable<GirModel.Union> unions, string path)
    {
        var publisher = new FilePublisher(path);

        foreach (var union in unions)
            Generate(union, publisher);
    }

    private static void Generate(GirModel.Union union, FilePublisher filePublisher)
    {
        var internalStructGenerator = new InternalUnionStructGenerator();
        var structCodeUnit = internalStructGenerator.Generate(union);
        filePublisher.Publish(structCodeUnit);
        
        if (union.Constructors.Any() || union.Functions.Any() || union.Methods.Any() || union.TypeFunction is not null)
        {
            var internalMethodsGenerator = new InternalUnionMethodsGenerator();
            var codeUnit = internalMethodsGenerator.Generate(union);
            filePublisher.Publish(codeUnit);   
        }
    }
}
