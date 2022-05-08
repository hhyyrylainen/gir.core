using Generator3.Converter;
using Generator3.Renderer.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class PublicBitfieldGenerator : Generator<GirModel.Bitfield>
{
    public CodeUnit Generate(GirModel.Bitfield obj)
    {
        var source = BitfieldRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), obj.Name, source);
    }
}
