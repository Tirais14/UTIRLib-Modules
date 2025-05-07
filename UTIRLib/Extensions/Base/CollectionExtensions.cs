using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace UTIRLib
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty([NotNullWhen(false)] this ICollection collection) => 
            ObjectHelper.IsNull(collection) || collection.Count == 0;

        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this ICollection collection) => 
            ObjectHelper.IsNotNull(collection) && collection.Count >= 0;
    }
}
