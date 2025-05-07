using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace UTIRLib
{
    public static class ArrayExtensions
    {
        public static T Find<T>(this T[] array, Predicate<T> matchPredicate) => Array.Find(array, matchPredicate);

        public static T? Find<T>(this T[] array, T value)
        {
            int index = Array.IndexOf(array, value);

            return index > -1 ? array[index] : default;
        }

        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);

        public static IEnumerator<T> GetEnumeratorT<T>(this T[] array) => new ArrayEnumerator<T>(array);

        public static bool HasNullOrEmptyString(this string[] strings)
        {
            int stringsLength = strings.Length;
            for (int i = 0; i < stringsLength; i++) {
                if (string.IsNullOrEmpty(strings[i])) {
                    return true;
                }
            }

            return false;
        }

        public static bool HasNullElement<T>(this T[] array)
        {
            int arrayLength = array.Length;
            for (int i = 0; i < arrayLength; i++) {
                if (ObjectHelper.IsNull(array[i])) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Counts the elements but ignore <see langword="null"/> values.
        /// </summary>
        public static int Quantity<T>(this T[] array)
        {
            int arrayLength = array.Length;
            int quantity = 0;
            for (int i = 0; i < arrayLength; i++) {
                if (ObjectHelper.IsNotNull(array[i])) {
                    quantity++;
                }
            }

            return quantity;
        }
        /// <summary>
        /// Counts the elements but ignore <see langword="null"/> values.
        /// </summary>
        public static int Quantity(this Array array)
        {
            int quantity = 0;
            foreach (var element in array) {
                if (ObjectHelper.IsNotNull(element)) {
                    quantity++;
                }
            }

            return quantity;
        }

        public static bool IsEmpty(this Array array) => array.Length == 0;

        public static bool IsNotEmpty(this Array array) => array.Length > 0;

        public static bool IsNullOrEmpty([NotNullWhen(false)] this Array? array) => array == null || array.Length == 0;

        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Array? array) => array != null && array.Length > 0;
    }
}
