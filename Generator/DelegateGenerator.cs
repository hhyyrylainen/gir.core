﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using GirLoader.Output.Model;

namespace Generator
{
    internal partial class DelegateGenerator
    {
        public static string WriteCallbackMarshallerParams(ParameterList parameterList, ReturnValue returnValue)
        {
            // This part is a bit problematic. We can't use type inference (or lambdas
            // for that matter) for in/out/ref parameters, of which we have several.
            //
            // The solution is to change from using lambdas to inline delegates. We will
            // have to know the exact type of each parameter to write this, meaning we
            // will need to know whether a type is under the top-level namespace (e.g.
            // Gtk.Button) or the native-level namespace (e.g. Gtk.Native.Button.Handle).
            //
            // TODO: For now, we just return "default!" if we can't generate it.
            if (!CanGenerateDelegate(parameterList, returnValue))
                return "default!;";

            var nativeParams = parameterList.SingleParameters.Select(x => x.Name);
            return "(" + string.Join(',', nativeParams) + ") =>";
        }

        public static string WriteCallbackMarshallerReturn(ParameterList parameterList, ReturnValue returnValue, string paramName, Namespace currentNamespace)
        {
            // If we can't generate, the exception generated by
            // WriteCallbackMarshallerBody means we don't need to
            // return anything. TODO: Generate all delegates (see above)
            if (!CanGenerateDelegate(parameterList, returnValue))
                return string.Empty;

            return Convert.ManagedToNative(
                transferable: returnValue,
                fromParam: paramName,
                currentNamespace: currentNamespace
            );
        }

        public static string WriteCallbackMarshallerBody(ParameterList parameterList, ReturnValue returnValue, Namespace currentNamespace)
        {
            if (!CanGenerateDelegate(parameterList, returnValue))
                return "throw new NotImplementedException(\"This exception is not supported\");";

            // TODO: Reuse parts of MethodGenerator for this
            // We'll need to think about freeing our unmanaged resources
            // so the block system is a good fit.

            var builder = new StringBuilder();
            var args = new List<string>();

            // Remove userData, closure data parameters
            var managedParams = parameterList.GetManagedParameters();

            foreach (Parameter arg in managedParams)
            {
                var expression = Convert.NativeToManaged(
                    transferable: arg,
                    fromParam: arg.SymbolName,
                    currentNamespace: currentNamespace,
                    useSafeHandle: false
                );

                builder.AppendLine($"var {arg.SymbolName}Managed = {expression};");
                args.Add(arg.SymbolName + "Managed");
            }

            var funcArgs = string.Join(
                separator: ", ",
                values: args
            );

            var funcCall = returnValue.IsVoid()
                ? $"managedCallback({funcArgs});"
                : $"var managedCallbackResult = managedCallback({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }

        private static bool CanGenerateDelegate(ParameterList parameterList, ReturnValue returnValue)
        {
            // We only support a subset of delegates at the
            // moment. Determine if we can generate based on
            // the following criteria:

            // No in/out/ref parameters
            if (parameterList.SingleParameters.Any(param => param.Direction != Direction.Default))
                return false;

            // No union parameters
            if (parameterList.SingleParameters.Any(param =>
                param.TypeReference.GetResolvedType() is Union))
                return false;

            // No GObject array parameters
            if (parameterList.SingleParameters.Any(param =>
                param.TypeReference.GetResolvedType() is Class &&
                param.TypeInformation.Array != null))
                return false;

            // No delegate return values
            if (returnValue.TypeReference.GetResolvedType() is Callback)
                return false;

            // No union return values
            if (returnValue.TypeReference.GetResolvedType() is Union)
                return false;

            // No GObject array return values
            if (returnValue.TypeReference.GetResolvedType() is Class &&
                returnValue.TypeInformation.Array != null)
                return false;

            // Go ahead and generate
            return true;
        }
    }
}
