using Generator3.Converter;
using Generator3.Renderer.V2.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Standard;

public class PublicInterfaceFrameworkGenerator : Generator<GirModel.Interface>
{
    public CodeUnit Generate(GirModel.Interface obj)
    {
        var source = InterfaceFrameworkRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), $"{obj.Name}.Framework", source);
    }
}
