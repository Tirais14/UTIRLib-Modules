using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class GameObjectExtensions
    {
        public static bool TryGetAssignedObject<T>(this GameObject gameObject, [NotNullWhen(true)] out T? result)
            where T : class => GameObjectHelper.TryGetAssignedObject(gameObject, out result);
        public static bool TryGetAssignedObject(this GameObject gameObject, Type targetType,
            [NotNullWhen(true)] out object? result) => GameObjectHelper.TryGetAssignedObject(
                gameObject, targetType, out result);

        public static bool TryGetAssignedObjects<T>(this GameObject gameObject, out T[] results)
             where T : class => GameObjectHelper.TryGetAssignedObjects(gameObject, out results);
        public static bool TryGetAssignedObjects(this GameObject gameObject, Type targetType, out object[] results) =>
            GameObjectHelper.TryGetAssignedObjects(gameObject, targetType, out results);

        public static T? GetAssignedObject<T>(this GameObject gameObject) where T : class =>
            GameObjectHelper.GetAssignedObject<T>(gameObject);
        public static object? GetAssignedObject(this GameObject gameObject, Type targetType) =>
            GameObjectHelper.GetAssignedObject(gameObject, targetType);

        public static T[] GetAssignedObjects<T>(this GameObject gameObject) where T : class =>
            GameObjectHelper.GetAssignedObjects<T>(gameObject);
        public static object[] GetAssignedObjects(this GameObject gameObject, Type targetType) =>
            GameObjectHelper.GetAssignedObjects(gameObject, targetType);
    }
}
