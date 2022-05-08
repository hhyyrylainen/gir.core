using Generator3.Converter;
using Generator3.Renderer.Public;
using Generator3.Renderer.V2.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Standard;

public class PublicClassMethodsGenerator : Generator<GirModel.Class>
{
    public CodeUnit Generate(GirModel.Class obj)
    {
        var source = ClassMethodsRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), $"{obj.Name}.Methods", source);
    }
}
