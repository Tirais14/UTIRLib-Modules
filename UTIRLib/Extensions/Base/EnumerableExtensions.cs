using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace UTIRLib
{
    public static class EnumerableExtensions
    {
        public static bool HasNullElement(this IEnumerable enumerable)
        {
            foreach (var item in enumerable) {
                if (ObjectHelper.IsNull(item)) {
                    return true;
                }
            }

            return false;
        }

        public static int Count(this IEnumerable enumerable)
        {
            int counter = 0;
            foreach (var item in enumerable) {
                counter++;
            }

            return counter;
        }

        public static bool IsNullOrEmpty([NotNullWhen(false)] this IEnumerable enumerable) =>
            ObjectHelper.IsNull(enumerable) || enumerable.Count() == 0;

        public static bool IsNotNullOrEmpty([NotNullWhen(false)] this IEnumerable enumerable) =>
            ObjectHelper.IsNotNull(enumerable) && enumerable.Count() > 0;
    }
}
