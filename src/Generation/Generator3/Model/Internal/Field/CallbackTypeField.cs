﻿using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class CallbackTypeField : Field
    {
        private GirModel.Callback Type => (GirModel.Callback) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => Type.Namespace.GetInternalName() + "." + Type.GetName();

        public CallbackTypeField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Callback>();
        }
    }
}
