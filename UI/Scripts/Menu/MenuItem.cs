using System;
using UniRx;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public abstract class MenuItem : MonoInitializable, IMenuItem
    {
        protected readonly ReactiveProperty<bool> isEnabledProp = new();
        protected Type[] supportedTypes = null!;
        [SerializeField] protected string menuItemName = null!;

        public IReadOnlyReactiveProperty<bool> IsEnabledProp => isEnabledProp;
        public ReadOnlyArray<Type> SupportedTypes => supportedTypes;
        public string MenuItemName => menuItemName;
        public bool IsEnabled => IsEnabledProp.Value;

        protected override void InitializeInternal() => supportedTypes = GetSupportedTypes();

        public void Show() => isEnabledProp.Value = false;

        public void Hide() => isEnabledProp.Value = true;

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

        public abstract void OnMenuItemClick();

        protected abstract Type[] GetSupportedTypes();
    }
}
