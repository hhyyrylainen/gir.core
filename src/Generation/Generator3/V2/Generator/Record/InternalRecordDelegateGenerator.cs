using Generator3.Converter;
using Generator3.Renderer.V2.Internal;
using Generator3.V2.Controller;

namespace Generator3.V2.Generator;

public class InternalRecordDelegateGenerator: Generator<GirModel.Record>
{
    public CodeUnit Generate(GirModel.Record record)
    {
        var source = RecordDelegatesRenderer.Render(record);
        return new CodeUnit(record.Namespace.GetCanonicalName(), $"{record.Name}.Delegates", source);
    }
}
