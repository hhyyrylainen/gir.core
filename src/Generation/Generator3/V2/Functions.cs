using System.Collections.Generic;
using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Functions
{
    public static void Generate(this IEnumerable<GirModel.Function> functions, string path)
    {
        var filePublisher = new FilePublisher(path);
        var generator = new InternalGlobalFunctionsGenerator();
        var codeUnit = generator.Generate(functions);
        
        filePublisher.Publish(codeUnit);
    }
}
