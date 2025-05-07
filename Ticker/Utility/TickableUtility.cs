using System;
using System.Reflection;
using UnityEngine;

namespace UTIRLib.CustomTicker
{
    using Diagnostics;
    public static class TickableUtility
    {
        private const string TICKABLE_NAME_PART = "tickable";

        public static bool HasCustomTickableAttribute<T>() => HasCustomTickableAttribute(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool HasCustomTickableAttribute(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return false;
            }

            return HasCustomTickableAttribute(obj.GetType());
        }
        public static bool HasCustomTickableAttribute(Type type)
        {
            if (type == null) {
                return false;
            }

            return type.IsDefined(typeof(CustomTickableAttribute));
        }

        public static bool TryGetCustomTickableAttribute<T>(out CustomTickableAttribute useCustomTickerAttribute)
        {
            useCustomTickerAttribute = GetCustomTickableAttribute<T>();
            return useCustomTickerAttribute != null;
        }
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool TryGetCustomTickableAttribute(object obj, out CustomTickableAttribute useCustomTickerAttribute)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                useCustomTickerAttribute = null;
                return false;
            }

            useCustomTickerAttribute = GetCustomTickableAttribute(obj);
            return useCustomTickerAttribute != null;
        }
        public static bool TryGetCustomTickableAttribute(Type type, out CustomTickableAttribute useCustomTickerAttribute)
        {
            useCustomTickerAttribute = GetCustomTickableAttribute(type);
            return useCustomTickerAttribute != null;
        }

        public static CustomTickableAttribute GetCustomTickableAttribute<T>() =>
           GetCustomTickableAttribute(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static CustomTickableAttribute GetCustomTickableAttribute(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return null;
            }

            return GetCustomTickableAttribute(obj.GetType());
        }

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static CustomTickableAttribute GetCustomTickableAttribute(Type type)
        {
            if (type == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(type)));
                return null;
            }

            return type.GetCustomAttribute<CustomTickableAttribute>();
        }

        public static bool IsTickable<T>() => IsTickable(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool IsTickable(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return false;
            }

            return IsTickable(obj.GetType());
        }

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool IsTickable(Type type)
        {
            if (type == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(type)));
                return false;
            }

            if (type.Name.Contains(TICKABLE_NAME_PART, StringComparison.InvariantCultureIgnoreCase)) {
                return true;
            }

            return TryGetTickableInterfaces(type, out _);
        }

        public static bool TryGetTickableInterfaces<T>(out Type[] tickableInterfaces)
        {
            tickableInterfaces = GetTickableInterfaces<T>();
            return tickableInterfaces != null && tickableInterfaces.Length > 0;
        }
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static bool TryGetTickableInterfaces(object obj, out Type[] tickableInterfaces)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                tickableInterfaces = Array.Empty<Type>();
                return false;
            }

            tickableInterfaces = GetTickableInterfaces(obj);
            return tickableInterfaces != null && tickableInterfaces.Length > 0;
        }
        public static bool TryGetTickableInterfaces(Type type, out Type[] tickableInterfaces)
        {
            tickableInterfaces = GetTickableInterfaces(type);
            return tickableInterfaces != null && tickableInterfaces.Length > 0;
        }

        public static Type[] GetTickableInterfaces<T>() => GetTickableInterfaces(typeof(T));
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static Type[] GetTickableInterfaces(object obj)
        {
            if (obj == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(obj)));
                return Array.Empty<Type>();
            }

            return GetTickableInterfaces(obj.GetType());
        }
        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static Type[] GetTickableInterfaces(Type type)
        {
            if (type == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(type)));
                return null;
            }

            static bool filter(Type interfaceType, object obj) =>
                interfaceType.Name.StartsWith("i", StringComparison.InvariantCultureIgnoreCase) &&
                interfaceType.Name.Contains((string)obj, StringComparison.InvariantCultureIgnoreCase) &&
                interfaceType.Namespace == typeof(ITickable).Namespace;

            Type[] tickableInterfaces = type.FindInterfaces(filter, TICKABLE_NAME_PART);

            return tickableInterfaces != null && tickableInterfaces.Length > 0 ? tickableInterfaces :
                Array.Empty<Type>();
        }
    }
}