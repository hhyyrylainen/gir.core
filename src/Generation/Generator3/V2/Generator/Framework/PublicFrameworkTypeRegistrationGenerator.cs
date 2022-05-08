using Generator3.Converter;
using Generator3.Renderer.V2.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class PublicFrameworkTypeRegistrationGenerator : Generator<GirModel.Namespace>
{
    public CodeUnit Generate(GirModel.Namespace ns)
    {
        var source = FrameworkTypeRegistrationRenderer.Render(ns);
        return new CodeUnit(ns.GetCanonicalName(), "Module.TypeRegistration", source);
    }
}
