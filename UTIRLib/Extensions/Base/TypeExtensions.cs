using System;
using System.Reflection;

#nullable enable
namespace UTIRLib
{
    public static class TypeExtensions
    {
        public static bool HasIndexer(this Type type)
        {
            PropertyInfo[] typeProperties = type.GetProperties();
            for (int i = 0; i < typeProperties.Length; i++)
            { if (typeProperties[i].GetIndexParameters().Length > 0) return true; }

            return false;
        }

        public static bool IsOrSubclassOf(this Type type, Type other) => type == other ||
            type.IsSubclassOf(other);
    }
}