using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.V2.Internal;

public static class DocCommentsRenderer
{
    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Parameter parameter) =>
        $@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";

    public static string Render(GirModel.ReturnType returnType) =>
        $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";
}
