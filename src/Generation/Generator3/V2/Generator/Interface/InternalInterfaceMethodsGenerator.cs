using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Standard;

public class InternalInterfaceMethodsGenerator : Generator<GirModel.Interface>
{
    public CodeUnit Generate(GirModel.Interface obj)
    {
        var source = InterfaceMethodsRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), obj.Name, source);
    }
}
