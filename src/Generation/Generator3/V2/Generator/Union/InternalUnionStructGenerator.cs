using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator.Union;

public class InternalUnionStructGenerator : Generator<GirModel.Union>
{
    public CodeUnit Generate(GirModel.Union union)
    {
        var source = UnionStructRenderer.Render(union);
        return new CodeUnit(union.Namespace.GetCanonicalName(), union.GetInternalStructName(), source);
    }
}
