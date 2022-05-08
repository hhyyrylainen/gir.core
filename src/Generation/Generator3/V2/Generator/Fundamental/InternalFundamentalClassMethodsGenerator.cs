using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Class.Fundamental;

public class InternalFundamentalClassMethodsGenerator : Generator<GirModel.Class>
{
    public CodeUnit Generate(GirModel.Class obj)
    {
        var source = FundamentalClassMethodsRenderer.Render(obj);
        return new CodeUnit(obj.Namespace.GetCanonicalName(), $"{obj.Name}.Instance.Methods", source);
    }
}
