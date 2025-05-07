using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    public static class ComponentHelper
    {
        #region Assign Methods
        /// <param name="componentType"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponent<TValue>(Component source, [NotNull] ref TValue? value, Type componentType)
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (value.IsNotNull()) return;

            value = GetComponentFrom<Component, TValue>(source.GetComponent(componentType));

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponent<TSource, TComponent, TValue>(TSource source, [NotNull] ref TValue? value)
            where TSource : Component
            where TComponent : Component
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (value.IsNotNull()) return;

            value = GetComponentFrom<TComponent, TValue>(source.GetComponent<TComponent>());

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        public static void AssignComponent<TSource, T>(TSource source, [NotNull] ref T? value)
            where TSource : Component
            where T : Component => AssignComponent<TSource, T, T>(source, ref value);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponentFromParent<TValue>(Component source, [NotNull] ref TValue? value, Type componentType,
            bool includeInactive = true)
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (componentType == null) {
                throw new ArgumentNullException(nameof(componentType));
            }
            if (value.IsNotNull()) return;

            value = GetComponentFrom<Component, TValue>(source.GetComponentInParent(componentType, includeInactive));

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponentFromParent<TSource, TComponent, TValue>(TSource source, [NotNull] ref TValue? value,
            bool includeInactive = true)
            where TSource : Component
            where TComponent : Component
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (value.IsNotNull()) return;

            value = GetComponentFrom<TComponent, TValue>(source.GetComponentInParent<TComponent>(includeInactive));

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        public static void AssignComponentFromParent<TSource, T>(TSource source, [NotNull] ref T? value, 
            bool includeInactive = true)
            where TSource : Component
            where T : Component => AssignComponentFromParent<TSource, T, T>(source, ref value, includeInactive);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponentFromChildren<TValue>(Component source, [NotNull] ref TValue? value,
            Type componentType, bool includeInactive = true, string? childName = null)
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (value.IsNotNull()) return;

            if (string.IsNullOrEmpty(childName)) {
                value = GetComponentFrom<Component, TValue>(source.GetComponentInChildren(componentType, includeInactive));
            }
            else {
                Transform childTransform = source.transform.Find(childName);
                if (childTransform == null) {
                    throw new ObjectNotFoundException(childName, typeof(Transform));
                }
                value = childTransform.GetComponent(componentType).ConvertToType<TValue>();
            }

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        public static void AssignComponentFromChildren<TValue>(Component source, [NotNull] ref TValue? value,
            Type componentType, Enum childNameEnum, bool includeInactive = true)
            where TValue : class => AssignComponentFromChildren(source, ref value, componentType, includeInactive,
                    Enum.GetName(childNameEnum.GetType(), childNameEnum));
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void AssignComponentFromChildren<TSource, TComponent, TValue>(TSource source, 
            [NotNull] ref TValue? value, bool includeInactive = true, string? childName = null)
            where TSource : Component
            where TComponent : Component
            where TValue : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (value.IsNotNull()) return;

            if (string.IsNullOrEmpty(childName)) {
                value = GetComponentFrom<TComponent, TValue>(source.GetComponentInChildren<TComponent>(includeInactive));
            }
            else {
                Transform childTransform = source.transform.Find(childName);
                if (childTransform == null) {
                    throw new ObjectNotFoundException(childName, typeof(Transform));
                }
                value = childTransform.GetComponent<TComponent>().ConvertToType<TValue>();
            }

            if (value.IsNull()) {
                throw new ObjectNotFoundException(typeof(TValue));
            }
        }
        public static void AssignComponentFromChildren<TSource, TComponent, TValue, TEnum>(TSource source,
            [NotNull] ref TValue? value, TEnum childNameEnum, bool includeInactive = true)
            where TSource : Component
            where TComponent : Component
            where TValue : class
            where TEnum : Enum =>
                AssignComponentFromChildren<TSource, TComponent, TValue>(source, ref value, includeInactive, 
                    childNameEnum.ToString());
        public static void AssignComponentFromChildren<TSource, T>(TSource source,
            [NotNull] ref T? value, bool includeInactive = true, string? childName = null)
            where TSource : Component
            where T : Component =>
                AssignComponentFromChildren<TSource, T, T>(source, ref value, includeInactive,  childName);
        public static void AssignComponentFromChildren<TSource, T, TEnum>(TSource source,
            [NotNull] ref T? value, TEnum childNameEnum, bool includeInactive = true)
            where TSource : Component
            where T : Component
            where TEnum : Enum =>
                AssignComponentFromChildren<TSource, T, T>(source, ref value, includeInactive, childNameEnum.ToString());
        #endregion

        /// <summary>
        /// Same as basic, but if not found component - send log
        /// </summary>
        /// <remarks>Debug:
        /// <br/><see cref="ObjectNotFoundMessage"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static object? GetComponentFrom(Component source, Type toGetComponentType)
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (toGetComponentType == null) {
                throw new ArgumentNullException(nameof(toGetComponentType));
            }

            if (!source.TryGetComponent(toGetComponentType, out var found)) {
                Debug.LogError(new ObjectNotFoundMessage(toGetComponentType));
                return null;
            }

            return found;
        }

        /// <summary>
        /// Same as basic, but if not found component - send log
        /// </summary>
        /// <remarks>Debug:
        /// <br/><see cref="ObjectNotFoundMessage"/>
        /// </remarks>
        /// <typeparam name="TSource">May be <see langword="null"/>, but still send long</typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static TOut? GetComponentFrom<TSource, TOut>(TSource source)
            where TSource : Component
            where TOut : class
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.TryGetComponent<TOut>(out var found)) {
                Debug.LogError(new ObjectNotFoundMessage(typeof(TOut)));
                return null;
            }

            return found;
        }

        /// <summary>
        /// Try get or add specified <see cref="Component"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static T AddComponent<TSource, T>(TSource source, Type type)
            where TSource : Component
            where T : Component
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.TryGetComponent(type, out var foundComponent)) {
                return foundComponent.ConvertToType<T>();
            }
            else return source.gameObject.AddComponent(type).ConvertToType<T>();
        }
        /// <summary>
        /// Try get or add specified <see cref="Component"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static T AddComponent<TSource, T>(TSource source)
            where TSource : Component
            where T : Component
        {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.TryGetComponent<T>(out var foundComponent)) {
                return foundComponent;
            }
            else return source.gameObject.AddComponent<T>();
        }
        /// <summary>
        /// Try get or add specified <see cref="Component"/>
        /// </summary>
        public static void AddComponent<TSource, T>(TSource source, [NotNull] ref T? value)
            where TSource : Component
            where T : Component => value = AddComponent<TSource, T>(source, typeof(T));
    }
}
