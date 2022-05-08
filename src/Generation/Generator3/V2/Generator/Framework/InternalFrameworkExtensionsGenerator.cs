using Generator3.Converter;
using Generator3.Renderer.Public;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class InternalFrameworkExtensionsGenerator : Generator<GirModel.Namespace>
{
    public CodeUnit Generate(GirModel.Namespace ns)
    {
        var source = FrameworkExtensionsRenderer.Render(ns);
        return new CodeUnit(ns.GetCanonicalName(), "Extensions", source);
    }
}
