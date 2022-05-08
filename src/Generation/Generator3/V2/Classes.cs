using System.Collections.Generic;
using System.ComponentModel.Design;
using Generator3.V2.Controller;
using Generator3.V2.Generator.Class.Fundamental;
using Generator3.V2.Generator.Class.Standard;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Classes
{
    public static void Generate(this IEnumerable<GirModel.Class> classes, string path)
    {
        var publisher = new FilePublisher(path);
        
        foreach(var cls in classes)
            if(cls.IsFundamental)
                GenerateFundamental(cls, publisher);
            else
                GenerateStandard(cls, publisher);
    }

    private static void GenerateFundamental(GirModel.Class cls, FilePublisher publisher)
    {
        var generators = new List<Generator<GirModel.Class>>
        {
            new InternalFundamentalClassMethodsGenerator(),
            new InternalFundamentalClassStructGenerator(),
            new PublicFundamentalClassMethodsGenerator(),
            new PublicFundamentalFrameworkGenerator(),
        };

        foreach (var generator in generators)
        {
            var codeUnit = generator.Generate(cls);
            publisher.Publish(codeUnit);
        }
    }
    
    private static void GenerateStandard(GirModel.Class cls, FilePublisher publisher)
    {
        var generators = new List<Generator<GirModel.Class>>
        {
            new InternalClassMethodsGenerator(),
            new InternalClassStructGenerator(),
            new PublicClassConstructorsGenerator(),
            new PublicClassMethodsGenerator(),
            new PublicClassPropertiesGenerator(),
            new PublicClassFrameworkGenerator(),
            new PublicClassSignalsGenerator(),
        };

        foreach (var generator in generators)
        {
            var codeUnit = generator.Generate(cls);
            publisher.Publish(codeUnit);
        }
    }
}
