using System.Collections.Generic;
using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class PlatformSupport
{
    public static void GeneratePlatform(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace, string path)
    {
        var publisher = new FilePublisher(path);
        var generator = new InternalPlatformSupportGenerator();
        var codeUnit = generator.Generate(linuxNamespace, macosNamespace, windowsNamespace);
        publisher.Publish(codeUnit);
    }
}
