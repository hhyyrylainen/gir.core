using System.Collections.Generic;
using Generator3.V2.Generator;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Bitfields
{
    public static void Generate(this IEnumerable<GirModel.Bitfield> bitfields, string path)
    {
        var publisher = new FilePublisher(path);
        var generator = new PublicBitfieldGenerator();

        foreach (var bitfield in bitfields)
        {
            var codeUnit = generator.Generate(bitfield);
            publisher.Publish(codeUnit);
        }
    }
}
