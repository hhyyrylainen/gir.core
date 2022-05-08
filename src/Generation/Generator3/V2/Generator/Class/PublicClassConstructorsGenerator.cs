using Generator3.Converter;
using Generator3.Renderer.V2.Public;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Standard;

public class PublicClassConstructorsGenerator : Generator<GirModel.Class>
{
    public CodeUnit Generate(GirModel.Class obj)
    {
        var source = ClassConstructorsRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), $"{obj.Name}.Constructors", source);
    }
}
