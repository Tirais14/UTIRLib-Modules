using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UTIRLib.InputSystem;

#nullable enable
namespace UTIRLib.UI
{
    public class UIRaycaster : IRaycaster
    {
        protected readonly List<RaycastResult> raycastResults = new();
        protected readonly IPointerHandler pointerHandler;
        protected readonly PointerEventData eventData;
        protected readonly GraphicRaycaster graphicRaycaster;

        public UIRaycaster(IPointerHandler pointerHandler, GraphicRaycaster graphicRaycaster, EventSystem eventSystem)
        {
            this.pointerHandler = pointerHandler;
            this.graphicRaycaster = graphicRaycaster;
            eventData = new PointerEventData(eventSystem);
        }

        public bool TryRaycastFirst<T>([NotNullWhen(true)] out T? result, object? exclude = null)
             where T : class => TryRaycastFirst(pointerHandler.PointerPosition, out result, exclude);
        public bool TryRaycastFirst<T>(Vector2 position, [NotNullWhen(true)] out T? result,
            object? exclude = null) where T : class
        {
            result = RaycastFirst<T>(position, exclude);

            return result != null;
        }
        public bool TryRaycastFirst(Type targetType, [NotNullWhen(true)] out object? result, object? exclude = null) =>
            TryRaycastFirst(targetType, pointerHandler.PointerPosition, out result, exclude);
        public bool TryRaycastFirst(Type targetType, Vector2 position, [NotNullWhen(true)] out object? result,
            object? exclude = null)
        {
            result = RaycastFirst(targetType, position, exclude);

            return result != null;
        }

        public bool TryRaycast<T>(out T[] results, object? exclude = null) =>
            TryRaycast(pointerHandler.PointerPosition, out results, exclude);
        public bool TryRaycast<T>(Vector2 position, out T[] results, object? exclude = null)
        {
            results = Raycast(typeof(T), position, exclude) as T[] ?? Array.Empty<T>();

            return results.Length > 0;
        }
        public bool TryRaycast(Type targetType, out object[] results, object? exclude = null) =>
            TryRaycast(targetType, pointerHandler.PointerPosition, out results, exclude);
        public bool TryRaycast(Type targetType, Vector2 position, out object[] results, object? exclude = null)
        {
            results = Raycast(targetType, position, exclude);

            return results.Length > 0;
        }

        public T? RaycastFirst<T>(object? exclude = null)
            where T : class => RaycastFirst<T>(pointerHandler.PointerPosition, exclude);
        public T? RaycastFirst<T>(Vector2 position, object? exclude = null)
            where T : class => RaycastFirst(typeof(T), position, exclude) is T typedResult ? typedResult : default;
        public object? RaycastFirst(Type targetType, object? exclude = null) =>
            RaycastFirst(targetType, pointerHandler.PointerPosition, exclude);
        public object? RaycastFirst(Type targetType, Vector2 position, object? exclude = null) =>
            RaycastInternal(targetType, position, exclude, onlyFirst: false).FirstOrDefault();

        public T[] Raycast<T>(Vector2 position, object? exclude = null) where T : class => Raycast<T>(position, exclude);
        public T[] Raycast<T>(object? exclude = null) where T : class => Raycast<T>(pointerHandler.PointerPosition, exclude);
        public object[] Raycast(Type targetType, object? exclude = null) =>
            Raycast(targetType, pointerHandler.PointerPosition, exclude);
        public object[] Raycast(Type targetType, Vector2 position, object? exclude = null) =>
            RaycastInternal(targetType, position, exclude, onlyFirst: false);

        protected void RaycastRaw() => graphicRaycaster.Raycast(eventData, raycastResults);

        protected void SetRaycastPosition(Vector2 position) => eventData.position = position;

        protected void ResetResults() => raycastResults.Clear();

        private object[] RaycastInternal(Type targetType, Vector2 position, object? exclude, bool onlyFirst)
        {
            ResetResults();
            SetRaycastPosition(position);
            RaycastRaw();

            bool isExclude = exclude != null;
            int raycastResultsCount = raycastResults.Count;
            List<object> foundObjects = new();
            for (int i = 0; i < raycastResultsCount; i++) {
                if (raycastResults[i].gameObject.TryGetAssignedObject(targetType, out object? obj)) {
                    if (isExclude && !obj.Equals(exclude)) {
                        continue;
                    }

                    foundObjects.Add(obj);
                    if (onlyFirst) {
                        break;
                    }
                }
            }

            return foundObjects.ToArray();
        }
    }
}
