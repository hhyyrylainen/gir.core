﻿using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Record
{
    public class InternalMethodsModel
    {
        private readonly GirModel.Record _record;

        public string Name => _record.Name;
        public string NamespaceName => _record.Namespace.GetInternalName();
        public GirModel.PlatformDependent? PlatformDependent => _record as GirModel.PlatformDependent;

        public IEnumerable<Function> Functions { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Constructor> Constructors { get; }
        public Function? TypeFunction { get; }

        public InternalMethodsModel(GirModel.Record record)
        {
            _record = record;

            Functions = record.Functions
                .Select(function => new Function(function))
                .ToList();

            Methods = record.Methods
                .Select(method => new Method(method, record.Name))
                .ToList();

            Constructors = record.Constructors
                .Select(method => new Constructor(method))
                .ToList();

            TypeFunction = record.TypeFunction is not null
                ? new Function(record.TypeFunction)
                : null;
        }
    }
}
