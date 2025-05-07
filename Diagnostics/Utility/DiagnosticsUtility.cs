using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UTIRLib.Diagnostics
{
#nullable enable
    public static class DiagnosticsUtility
    {
        public static string ResolveNullOrEmptyText(object? obj) => obj.IsNull() ? "null" : "empty";

        public static string ToFormattedParameterName(string paramName) => ' ' + paramName;

        public static string ResolveTypeNameByObject(object obj) =>
            obj switch {
                Component => "Component",
                Object => "Unity object",
                _ => "Object"
            };
        public static string ResolveTypeNameBySystemType(Type type)
        {
            if (type == null) return "null";

            if (type == typeof(Component)) {
                return "Component";
            }
            else if (type.IsSubclassOf(typeof(Component))) {
                return $"Component{type.Name}";
            }
            else if (type == typeof(Object)) {
                return "Unity object";
            }
            else {
                return "Object";
            }
        }
    }
}
