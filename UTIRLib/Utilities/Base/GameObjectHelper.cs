using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class GameObjectHelper
    {
        public static bool TryGetAssignedObject<T>(GameObject gameObject, [NotNullWhen(true)] out T? result)
            where T : class
        {
            result = GetAssignedObject<T>(gameObject);

            return result != null;
        }
        public static bool TryGetAssignedObject(GameObject gameObject, Type targetType,
            [NotNullWhen(true)] out object? result)
        {
            result = GetAssignedObject(gameObject, targetType);

            return result != null;
        }

        public static bool TryGetAssignedObjects<T>(GameObject gameObject, out T[] results)
             where T : class
        {
            results = GetAssignedObjects<T>(gameObject);

            return results.Length > 0;
        }
        public static bool TryGetAssignedObjects(GameObject gameObject, Type targetType, out object[] results)
        {
            results = GetAssignedObjects(gameObject, targetType);

            return results.Length > 0;
        }

        public static T? GetAssignedObject<T>(GameObject gameObject) where T : class =>
            GetAssignedObject(gameObject, typeof(T)) is T typedResult ? typedResult : default;
        public static object? GetAssignedObject(GameObject gameObject, Type targetType)
        {
            GetAssignedObjectsValidation(gameObject, targetType);

            return GetAssignedObjectsInternal(gameObject, targetType, onlyFirst: true).FirstOrDefault();
        }

        public static T[] GetAssignedObjects<T>(GameObject gameObject) where T : class => 
            GetAssignedObjects(gameObject, typeof(T)) as T[] ?? Array.Empty<T>();
        public static object[] GetAssignedObjects(GameObject gameObject, Type targetType)
        {
            GetAssignedObjectsValidation(gameObject, targetType);

            return GetAssignedObjectsInternal(gameObject, targetType, onlyFirst: false);
        }

        private static object[] GetAssignedObjectsInternal(GameObject gameObject, Type targetType, bool onlyFirst)
        {
            List<object> results = new();
            Component[] gameObjectComponents = gameObject.GetComponents(typeof(Component)) ?? Array.Empty<Component>();
            int gameObjectComponentsCount = gameObjectComponents.Length;
            for (int i = 0; i < gameObjectComponentsCount; i++) {
                if (targetType.IsInstanceOfType(gameObjectComponents[i])) {
                    results.Add(gameObjectComponents[i]);
                    if (onlyFirst) {
                        break;
                    }
                }
            }

            return results.ToArray();
        }

        private static void GetAssignedObjectsValidation(GameObject gameObject, Type targetType)
        {
            if (gameObject == null) {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (targetType == null) {
                throw new ArgumentNullException(nameof(targetType));
            }
        }
    }
}
