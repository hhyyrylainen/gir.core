using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Generator3.V2.Controller;
using Generator3.V2.Generator;
using Generator3.V2.Generator.Class.Fundamental;
using Generator3.V2.Generator.Class.Standard;
using Generator3.V2.Publisher;

namespace Generator3.V2;

public static class Records
{
    public static void Generate(this IEnumerable<GirModel.Record> records, string path)
    {
        var publisher = new FilePublisher(path);

        var delegateGenerator = new InternalRecordDelegateGenerator();

        foreach (var record in records)
        {
            if (record.Fields.Any(field => field.AnyTypeOrCallback.IsT1))
            {
                var codeUnit = delegateGenerator.Generate(record);
                publisher.Publish(codeUnit);
            }
        }
    }
}
