using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Standard;

public class InternalClassStructGenerator : Generator<GirModel.Class>
{
    public CodeUnit Generate(GirModel.Class obj)
    {
        var source = ClassStructRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), obj.GetInternalStructName(), source);
    }
}
