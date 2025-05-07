using System;
using UnityEngine;

namespace UTIRLib.CustomTicker
{
    using Diagnostics;
    public static class TickerUtility
    {
        private const string TICKER_NAME_PART = "ticker";

        public static bool IsTicker<T>() => IsTicker(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool IsTicker(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return false;
            }

            return IsTicker(obj.GetType());
        }

        public static bool IsTicker(Type type)
        {
            if (type.Name.Contains(TICKER_NAME_PART, StringComparison.OrdinalIgnoreCase) &&
                type.Namespace == typeof(Ticker).Namespace) {
                return true;
            }

            return TryGetTickerInterfaces(type, out _);
        }

        public static bool TryGetTickerInterfaces<T>(out Type[] tickerInterfaces)
        {
            tickerInterfaces = GetTickerInterfaces<T>();
            return tickerInterfaces != null && tickerInterfaces.Length > 0;
        }
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool TryGetTickerInterfaces(object obj, out Type[] tickerInterfaces)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                tickerInterfaces = Array.Empty<Type>();
                return false;
            }

            tickerInterfaces = GetTickerInterfaces(obj);
            return tickerInterfaces != null && tickerInterfaces.Length > 0;
        }
        public static bool TryGetTickerInterfaces(Type type, out Type[] tickerInterfaces)
        {
            tickerInterfaces = GetTickerInterfaces(type);
            return tickerInterfaces != null && tickerInterfaces.Length > 0;
        }

        public static Type[] GetTickerInterfaces<T>() => GetTickerInterfaces(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static Type[] GetTickerInterfaces(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return Array.Empty<Type>();
            }

            return GetTickerInterfaces(obj.GetType());
        }
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static Type[] GetTickerInterfaces(Type type)
        {
            if (type == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(type)));
                return null;
            }

            static bool filter(Type interfaceType, object obj) =>
                interfaceType.Name.StartsWith("i", StringComparison.OrdinalIgnoreCase) &&
                interfaceType.Name.Contains((string)obj, StringComparison.OrdinalIgnoreCase) &&
                interfaceType.Namespace == typeof(ITicker).Namespace;

            Type[] tickerInterfaces = type.FindInterfaces(filter, TICKER_NAME_PART);

            return tickerInterfaces != null && tickerInterfaces.Length > 0 ? tickerInterfaces :
                Array.Empty<Type>();
        }
    }
}