using UniRx;
using UnityEngine.InputSystem.Utilities;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public class MenuViewModel : MonoInitializable<Menu>
    {
        protected readonly ReactiveCollection<IMenuItem> menuItemsView = new();
        protected Menu menu = null!;

        public IReadOnlyReactiveCollection<IMenuItem> MenuItemsView => menuItemsView;
        public IReadOnlyReactiveProperty<bool> OpenedView => menu.IsOpened;

        protected override void InitializeInternal(Menu arg)
        {
            menu = arg;
            Bind();
        }

        private void OnTargetChanged(object? target)
        {
            menuItemsView.Clear();

            if (target.IsNotNull()) {
                ReadOnlyArray<IMenuItem> targetMenuItems = menu.TargetMenuItems;
                for (int i = 0; i < targetMenuItems.Count; i++) {
                    menuItemsView.Add(targetMenuItems[i]);
                }
            }
        }

        private void Bind()
        {
            menu.Target.Subscribe(OnTargetChanged).AddTo(this);
        }
    }
}
