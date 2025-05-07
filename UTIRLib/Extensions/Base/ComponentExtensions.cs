using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib
{
    public static class ComponentExtensions
    {
        public static bool TryGetAssignedObject<T>(this Component component, [NotNullWhen(true)] out T? result)
            where T : class => GameObjectHelper.TryGetAssignedObject(component.gameObject, out result);
        public static bool TryGetAssignedObject(this Component component, Type targetType,
            [NotNullWhen(true)] out object? result) => GameObjectHelper.TryGetAssignedObject(
                component.gameObject, targetType, out result);

        public static bool TryGetAssignedObjects<T>(this Component component, out T[] results)
             where T : class => GameObjectHelper.TryGetAssignedObjects(component.gameObject, out results);
        public static bool TryGetAssignedObjects(this Component component, Type targetType, out object[] results) =>
            GameObjectHelper.TryGetAssignedObjects(component.gameObject, targetType, out results);

        public static T? GetAssignedObject<T>(this Component component) where T : class =>
            GameObjectHelper.GetAssignedObject<T>(component.gameObject);
        public static object? GetAssignedObject(this Component component, Type targetType) =>
            GameObjectHelper.GetAssignedObject(component.gameObject, targetType);

        public static T[] GetAssignedObjects<T>(this Component component) where T : class =>
            GameObjectHelper.GetAssignedObjects<T>(component.gameObject);
        public static object[] GetAssignedObjects(this Component component, Type targetType) =>
            GameObjectHelper.GetAssignedObjects(component.gameObject, targetType);

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static T? AddComponent<T>(this Component component)
            where T : Component
        {
            if (component == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(component)));
                return null;
            }

            return ComponentHelper.AddComponent<Component, T>(component);
        }
    }
}
