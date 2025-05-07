using System;
using UnityEngine;

namespace UTIRLib
{
    public static partial class MonoBehaviourExtensions
    {
        public static void AssignComponent<TValue>(this MonoBehaviour source, ref TValue value,
            Type componentType)
            where TValue : class => ComponentHelper.AssignComponent(source, ref value, componentType);
        public static void AssignComponent<TComponent, TValue>(this MonoBehaviour source, 
            ref TValue value)
            where TComponent : Component
            where TValue : class =>
                ComponentHelper.AssignComponent<MonoBehaviour, TComponent, TValue>(source,
                    ref value);
        public static void AssignComponent<T>(this MonoBehaviour source, ref T value)
            where T : Component => ComponentHelper.AssignComponent(source, ref value);

        public static void AssignComponentFromParent<TValue>(this MonoBehaviour source, ref TValue value,
            Type componentType, bool includeInactive = true)
            where TValue : class =>
                ComponentHelper.AssignComponentFromParent(source, ref value, componentType, includeInactive);
        public static void AssignComponentFromParent<TComponent, TValue>(this MonoBehaviour source,
            ref TValue value, bool includeInactive = true)
            where TComponent : Component
            where TValue : class =>
                ComponentHelper.AssignComponentFromParent<MonoBehaviour, TComponent, TValue>(
                    source, ref value, includeInactive);
        public static void AssignComponentFromParent<T>(this MonoBehaviour source, ref T value, 
            bool includeInactive = true)
            where T : Component =>
                ComponentHelper.AssignComponentFromParent(source, ref value, includeInactive);

        public static void AssignComponentFromChildren<TValue>(this MonoBehaviour source,
            ref TValue value, Type componentType, bool includeInactive = true,
            string childName = null)
            where TValue : class => ComponentHelper.AssignComponentFromChildren(source, ref value,
                componentType, includeInactive, childName);
        public static void AssignComponentFromChildren<TValue>(this MonoBehaviour source,
            ref TValue value, Type componentType, Enum childNameEnum,
            bool includeInactive = true)
            where TValue : class => ComponentHelper.AssignComponentFromChildren(source, ref value,
                componentType, childNameEnum, includeInactive);
        public static void AssignComponentFromChildren<TComponent, TValue>(this MonoBehaviour source,
            ref TValue value, bool includeInactive = true, string childName = null)
            where TComponent : Component
            where TValue : class =>
                ComponentHelper.AssignComponentFromChildren<MonoBehaviour, TComponent, TValue>(
                    source, ref value, includeInactive, childName);
        public static void AssignComponentFromChildren<TComponent, TValue, TEnum>(
            this MonoBehaviour source, ref TValue value, TEnum childNameEnum,
            bool includeInactive = true)
            where TComponent : Component
            where TValue : class
            where TEnum : Enum =>
                ComponentHelper.AssignComponentFromChildren<MonoBehaviour, TComponent, TValue, TEnum>(
                    source, ref value, childNameEnum, includeInactive);
        public static void AssignComponentFromChildren<T>(this MonoBehaviour source, ref T value,
            bool includeInactive = true, string childName = null)
            where T : Component =>
                ComponentHelper.AssignComponentFromChildren(source, ref value, includeInactive,
                    childName);
        public static void AssignComponentFromChildren<T, TEnum>(this MonoBehaviour source, ref T value,
            TEnum childNameEnum, bool includeInactive = true)
            where T : Component
            where TEnum : Enum =>
                ComponentHelper.AssignComponentFromChildren(source, ref value, childNameEnum,
                    includeInactive);
    }
}
