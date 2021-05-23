﻿namespace GirLoader.Output.Model
{
    public class Float : PrimitiveValueType
    {
        public Float(string ctypeName) : base(new CTypeName(ctypeName), new SymbolName("float")) { }
    }
}
