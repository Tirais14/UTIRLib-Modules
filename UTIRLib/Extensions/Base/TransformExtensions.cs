using System;
using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class TransformExtensions
    {
        /// <summary>Finds a child <see cref="Transform"/> by enum name and returns it</summary>
        public static Transform? Find<T>(this Transform transform, T enumValue)
            where T : Enum => transform.Find(enumValue.ToString());

        /// <summary>Finds a child <see cref="GameObject"/> by enum name and returns it</summary>
        public static GameObject? FindGameObject(this Transform transform, string name)
        {
            Transform foundTransform = transform.Find(name);
            return foundTransform != null ? foundTransform.gameObject : null;
        }
        /// <summary>Finds a child <see cref="GameObject"/> by enum name and returns it</summary>
        public static GameObject? FindGameObject<T>(this Transform transform, T enumValue)
            where T : Enum
        {
            Transform foundTransform = transform.Find(enumValue.ToString());
            return foundTransform != null ? foundTransform.gameObject : null;
        }

        /// <summary>Tries to find a <see cref="Transform"/> child by name</summary>
        public static bool TryFind(this Transform transform, string name, out Transform? result)
        {
            result = transform.Find(name);
            return result != null;
        }
        /// <summary>Tries to find a <see cref="Transform"/> child by enum name</summary>
        public static bool TryFind<TEnum>(this Transform transform, TEnum enumValue, out Transform? result)
            where TEnum : Enum
        {
            result = transform.Find(enumValue.ToString());
            return result != null;
        }
        /// <summary>Tries to find a <see cref="GameObject"/> child by name</summary>
        public static bool TryFindGameObject(this Transform transform, string name, out GameObject? result)
        {
            result = transform.FindGameObject(name);
            return result != null;
        }
        /// <summary>Tries to find a <see cref="GameObject"/> child by enum name</summary>
        public static bool TryFindGameObject<TEnum>(this Transform transform, TEnum enumValue,
            out GameObject? result)
            where TEnum : Enum
        {
            result = transform.FindGameObject(enumValue.ToString());
            return result != null;
        }

        /// <summary>Returns a <see cref="Transform"/> child by enum number value</summary>
        /// <remarks>Allowed enum number types:
        /// <br/><see cref="int"/>, <see cref="short"/>, <see cref="byte"/>
        /// </remarks>
        public static Transform? GetChild<TEnum>(this Transform transform, TEnum enumValue)
            where TEnum : Enum
        {
            if (enumValue is int index) {
                return transform.GetChild(index);
            }
            else if (enumValue is short shortIndex) {
                return transform.GetChild(shortIndex);
            }
            else if (enumValue is byte byteIndex) {
                return transform.GetChild(byteIndex);
            }

            Debug.LogError($"Unexpected enum number type {Enum.GetUnderlyingType(typeof(TEnum)).Name}");
            return null;
        }
        /// <summary>Tries to get a <see cref="Transform"/> child by index</summary>
        public static bool TryGetChild(this Transform transform, int index, out Transform? result)
        {
            int transformChildCount = transform.childCount;
            if (transformChildCount == 0) {
                result = null;
                return false;
            }
            else if (transformChildCount > -1 && index < transformChildCount) {
                result = transform.GetChild(index);
                return true;
            }

            result = null;
            return false;
        }
        /// <summary>Tries to get a <see cref="Transform"/> child by enum number value</summary>
        /// <remarks>Allowed enum number types:
        /// <br/><see cref="int"/>, <see cref="short"/>, <see cref="byte"/>
        /// </remarks>
        public static bool TryGetChild<TEnum>(this Transform transform, TEnum enumValue, out Transform? result)
            where TEnum : Enum
        {
            if (enumValue is int index) {
                return transform.TryGetChild(index, out result);
            }
            else if (enumValue is short shortIndex) {
                return transform.TryGetChild(shortIndex, out result);
            }
            else if (enumValue is byte byteIndex) {
                return transform.TryGetChild(byteIndex, out result);
            }

            result = null;
            return false;
        }
        /// <summary>Returns a <see cref="GameObject"/> child by enum number value</summary>
        public static GameObject? GetChildGameObject(this Transform transform, int index)
        {
            Transform childTransform = transform.GetChild(index);
            return childTransform != null ? childTransform.gameObject : null;
        }
        /// <summary>Returns a <see cref="GameObject"/> child by enum number value</summary>
        /// <remarks>Allowed enum number types:
        /// <br/><see cref="int"/>, <see cref="short"/>, <see cref="byte"/>
        /// </remarks>
        public static GameObject? GetChildGameObject<TEnum>(this Transform transform, TEnum enumValue)
            where TEnum : Enum
        {
            if (enumValue is int index) {
                return transform.GetChildGameObject(index);
            }
            else if (enumValue is short shortIndex) {
                return transform.GetChildGameObject(shortIndex);
            }
            else if (enumValue is byte byteIndex) {
                return transform.GetChildGameObject(byteIndex);
            }

            Debug.LogError($"Unexpected enum number type {Enum.GetUnderlyingType(typeof(TEnum)).Name}");
            return null;
        }

        /// <summary>Tries to get a <see cref="GameObject"/> child by enum number value</summary>
        public static bool TryGetChildGameObject(this Transform transform, int index, out GameObject? result)
        {
            if (transform.TryGetChild(index, out Transform? foundTransform)) {
                result = foundTransform!.gameObject;
                return true;
            }

            result = null;
            return false;
        }
        /// <summary>Tries to get a <see cref="GameObject"/> child by enum name</summary>
        public static bool TryGetChildGameObject<TEnum>(this Transform transform, TEnum enumValue, 
            out GameObject? result)
            where TEnum : Enum
        {
            if (transform.TryGetChild(enumValue, out Transform? foundTransform)) {
                result = foundTransform!.gameObject;
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>Tries to get <see cref="Transform"/> child by enum number value or by name</summary>
        public static Transform? GetOrFind<TEnum>(this Transform transform, TEnum enumValue)
            where TEnum : Enum
        {
            if (transform.TryGetChild(enumValue, out Transform? result)) {
                return result;
            }
            else {
                return transform.Find(enumValue.ToString());
            }
        }
        /// <summary>Tries to get <see cref="GameObject"/> child by enum number value or by name</summary>
        public static GameObject? GetOrFindGameObject<TEnum>(this Transform transform, TEnum enumValue)
            where TEnum : Enum
        {
            if (transform.TryGetChildGameObject(enumValue, out GameObject? result)) {
                return result;
            }
            else {
                return transform.FindGameObject(enumValue.ToString());
            }
        }

        /// <summary>Tries to get <see cref="Transform"/> child by enum number value or by name</summary>
        public static bool TryGetOrFind<TEnum>(this Transform transform, TEnum enumValue, out Transform? result)
            where TEnum : Enum
        {
            if (transform.TryGetChild(enumValue, out result)) {
                return true;
            }
            else {
                result = transform.Find(enumValue.ToString());
                return result != null;
            }
        }
        /// <summary>Tries to get <see cref="GameObject"/> child by enum number value or by name</summary>
        public static bool TryGetOrFindGameObject<TEnum>(this Transform transform, TEnum enumValue, 
            out GameObject? result)
            where TEnum : Enum
        {
            if (transform.TryGetChildGameObject(enumValue, out result)) {
                return true;
            }
            else {
                result = transform.FindGameObject(enumValue.ToString());
                return result != null;
            }
        }
    }
}
