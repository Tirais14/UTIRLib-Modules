using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public class MonoBehaviourExtended : MonoBehaviour
    {
        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }

        protected Transform[] GetChilds()
        {
            if (transform.childCount == 0) {
                return Array.Empty<Transform>();
            }

            int childCount = transform.childCount;
            var childs = new Transform[childCount];
            for (int i = 0; i < childCount; i++) {
                childs[i] = transform.GetChild(i);
            }

            return childs;
        }

        #region Assign Methods
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponent<TValue>([NotNull] ref TValue? value, Type componentType)
            where TValue : class => ComponentHelper.AssignComponent(this, ref value, componentType);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponent<TComponent, TValue>([NotNull] ref TValue? value)
            where TComponent : Component
            where TValue : class => 
                ComponentHelper.AssignComponent<MonoBehaviourExtended, TComponent, TValue>(this, ref value);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponent<T>([NotNull] ref T? value)
            where T : Component => ComponentHelper.AssignComponent(this, ref value);

        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromParent<TValue>([NotNull] ref TValue? value,
            Type componentType, bool includeInactive = true)
            where TValue : class =>
                ComponentHelper.AssignComponentFromParent(this, ref value, componentType, includeInactive);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromParent<TComponent, TValue>([NotNull] ref TValue? value,
            bool includeInactive = true)
            where TComponent : Component
            where TValue : class =>
                ComponentHelper.AssignComponentFromParent<MonoBehaviourExtended, TComponent, TValue>(
                    this, ref value, includeInactive);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromParent<T>([NotNull] ref T? value, bool includeInactive = true)
            where T : Component =>
                ComponentHelper.AssignComponentFromParent(this, ref value, includeInactive);

        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<TValue>([NotNull] ref TValue? value,
            Type componentType, bool includeInactive = true,
            string? childName = null)
            where TValue : class => ComponentHelper.AssignComponentFromChildren(this, ref value, componentType,
                includeInactive, childName);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<TValue>([NotNull] ref TValue? value,
            Type componentType, Enum childNameEnum,
            bool includeInactive = true)
            where TValue : class => ComponentHelper.AssignComponentFromChildren(this, ref value, componentType, childNameEnum,
                includeInactive);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<TComponent, TValue>([NotNull] ref TValue? value,
            bool includeInactive = true, string? childName = null)
            where TComponent : Component
            where TValue : class => 
                ComponentHelper.AssignComponentFromChildren<MonoBehaviourExtended, TComponent, TValue>(this, ref value,
                    includeInactive, childName);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<TComponent, TValue, TEnum>([NotNull] ref TValue? value,
            TEnum childNameEnum, bool includeInactive = true)
            where TComponent : Component
            where TValue : class
            where TEnum : Enum =>
                ComponentHelper.AssignComponentFromChildren<MonoBehaviourExtended, TComponent, TValue, TEnum>(this, ref value,
                    childNameEnum, includeInactive);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<T>([NotNull] ref T? value, bool includeInactive = true,
            string? childName = null)
            where T : Component =>
                ComponentHelper.AssignComponentFromChildren(this, ref value, includeInactive, childName);
        /// <summary>
        /// Shortcut of <see cref="ComponentHelper"/> method
        /// </summary>
        protected void AssignComponentFromChildren<T, TEnum>([NotNull] ref T? value, TEnum childNameEnum,
            bool includeInactive = true)
            where T : Component
            where TEnum : Enum =>
                ComponentHelper.AssignComponentFromChildren(this, ref value, childNameEnum, includeInactive);
        #endregion

        /// <summary>
        /// Shortcut to <see cref="ComponentHelper.AddComponent{TSource, T}(TSource, Type)"/>
        /// </summary>
        protected T? AddComponent<T>(Type type) where T : Component =>
            ComponentHelper.AddComponent<MonoBehaviour, T>(this, type);
        /// <summary>
        /// Shortcut to <see cref="ComponentHelper.AddComponent{TSource, T}(TSource)"/>
        /// </summary>
        protected T AddComponent<T>() where T : Component => 
            ComponentHelper.AddComponent<MonoBehaviour, T>(this);
        /// <summary>
        /// Shortcut to <see cref="ComponentHelper.AddComponent{TSource, T}(TSource)"/>
        /// </summary>
        protected void AddComponent<T>([NotNull] ref T? value) where T : Component =>
            ComponentHelper.AddComponent<MonoBehaviour, T>(this, ref value);

        protected void Awake() => OnAwake();

        protected void Start() => OnStart();
    }
}
