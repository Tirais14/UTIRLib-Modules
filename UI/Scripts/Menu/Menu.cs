using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public abstract class Menu : MonoInitializable, IMenu
    {
        protected readonly ReactiveProperty<object?> target = new();
        protected readonly ReactiveProperty<bool> isOpened = new();
        protected readonly List<IMenuItem> targetMenuItem = new();
        protected Type? targetType = null!;
        protected Type? previousTargetType = null!;
        protected Type[] supportedTypes = null!;
        protected IMenuItem[] menuItems = null!;
        [SerializeField] protected GameObject[] menuItemPrefabs = null!;

        public IReadOnlyReactiveProperty<object?> Target => target;
        public IReadOnlyReactiveProperty<bool> IsOpened => isOpened;
        public ReadOnlyArray<Type> SupportedTypes => supportedTypes;
        public ReadOnlyArray<IMenuItem> MenuItems => menuItems;
        public ReadOnlyArray<IMenuItem> TargetMenuItems => previousTargetType == null || previousTargetType != targetType ?
            GetTargetMenuItems() : targetMenuItem.ToArray();

        protected override void InitializeInternal()
        {
            InitializeMenuItem();
            supportedTypes = GetSupportedTypes();
        }

        public void Open(Vector2 position, object target)
        {
            targetType = target.GetType();
            this.target.Value = target;
            transform.position = position;
            isOpened.Value = true;
        }

        public bool TryOpen(Vector2 position, object target)
        {
            if (IsSupportedObject(target)) {
                Open(position, target);
                return true;
            }

            return false;
        }

        public void Close()
        {
            isOpened.Value = false;
            previousTargetType = targetType;
            targetType = null;
            target.Value = null;
        }

        public bool IsSupportedObject(object? obj)
        {
            if (obj.IsNull()) return false;

            Type objType = obj.GetType();
            Type supportedType;
            for (int i = 0; i < supportedTypes.Length; i++) {
                supportedType = supportedTypes[i];
                if (supportedType == objType || supportedType.IsInstanceOfType(obj)) {
                    return true;
                }
            }

            return false;
        }

        private IMenuItem[] GetTargetMenuItems()
        {
            targetMenuItem.Clear();
            IMenuItem menuItem;
            for (int i = 0; i < menuItems.Length; i++) {
                menuItem = menuItems[i];
                if (menuItem.IsSupportedObject(target)) {
                    targetMenuItem.Add(menuItem);
                }
            }

            return targetMenuItem.ToArray();
        }

        private void InitializeMenuItem()
        {
            menuItems = new IMenuItem[menuItemPrefabs.Length];
            for (int i = 0; i < menuItemPrefabs.Length; i++) {
                menuItems[i] = menuItemPrefabs[i].GetComponent<IMenuItem>();
            }
        }

        protected abstract Type[] GetSupportedTypes();
    }
}
