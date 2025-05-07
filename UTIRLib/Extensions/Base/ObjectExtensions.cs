using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace UTIRLib
{
    public static class ObjectExtensions
    {
        public static bool IsDefault<T>([NotNullWhen(false)] this T obj) => EqualityComparer<T>.Default.Equals(obj, default!);

        public static bool IsNotDefault<T>([NotNullWhen(true)] this T obj) => !EqualityComparer<T>.Default.Equals(obj, default!);

        public static string GetTypeName(this object? obj) => obj?.GetType().Name ?? "null";

        public static object ConvertToType(this object obj, Type conversionType) =>
            Convert.ChangeType(obj, conversionType);
        public static T ConvertToType<T>(this object? obj) => (T)Convert.ChangeType(obj, typeof(T));

        public static bool TryConvertToType(this object obj, Type conversionType, [NotNullWhen(true)] out object? result)
        {
            try {
                result = Convert.ChangeType(obj, conversionType);
                return true;
            }
            catch (Exception) {
                result = null;
                return false;
            }
        }
        public static bool TryConvertToType<TObj, T>(this TObj obj, [NotNullWhen(true)] out T? result)
        {
            if (obj is T typedObj) {
                result = typedObj;
                return true;
            }

            result = default;
            return false;
        }
    }
}
