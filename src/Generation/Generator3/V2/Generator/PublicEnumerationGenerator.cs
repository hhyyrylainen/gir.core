using Generator3.Converter;
using Generator3.Renderer.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class PublicEnumerationGenerator : Generator<GirModel.Enumeration>
{
    public CodeUnit Generate(GirModel.Enumeration obj)
    {
        var source = EnumerationRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), obj.Name, source);
    }
}
