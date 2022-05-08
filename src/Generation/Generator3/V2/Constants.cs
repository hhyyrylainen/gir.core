using System.Collections.Generic;
using System.Linq;
using Generator3.V2.Controller;
using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Constants
{
    public static void Generate(this IEnumerable<GirModel.Constant> constants, string path)
    {
        if (!constants.Any())
            return;
        
        var publisher = new FilePublisher(path);
        var generator = new PublicConstantsGenerator();
        var codeUnit =generator.Generate(constants);
        publisher.Publish(codeUnit);
    }
}
