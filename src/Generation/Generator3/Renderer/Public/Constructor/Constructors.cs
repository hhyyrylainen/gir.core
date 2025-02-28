﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Public
{
    public static class Constructors
    {
        public static string Render(this IEnumerable<Model.Public.Constructor> constructors)
        {
            return constructors
                .Select(constructor => constructor.Render())
                .Join(Environment.NewLine);
        }
    }
}
