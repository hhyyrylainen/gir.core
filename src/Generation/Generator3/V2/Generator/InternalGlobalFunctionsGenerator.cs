using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class InternalGlobalFunctionsGenerator: Generator<IEnumerable<GirModel.Function>>
{
    public CodeUnit Generate(IEnumerable<GirModel.Function> functions)
    {
        var source = GlobalFunctionsRenderer.Render(functions);
        return new CodeUnit(functions.First().Namespace.GetCanonicalName(), "Functions", source);
    }
}
