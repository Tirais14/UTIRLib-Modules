using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public class MenuView : MonoInitializable<MenuViewModel>
    {
        protected readonly List<Button> buttons = new();
        protected MenuViewModel menuViewModel = null!;
        protected Image image = null!;

        protected override void InitializeInternal(MenuViewModel menuViewModel)
        {
            AssignComponent(ref image);
            this.menuViewModel = menuViewModel;
            Bind();
        }

        private void SwitchState(bool isEnabled)
        {
            if (isEnabled) {
                ShowMenuItems();
            }
            else {
                HideMenuItems();
            }

            enabled = isEnabled;
        }

        private void ShowMenuItems()
        {
            for (int i = 0; i < menuViewModel.MenuItemsView.Count; i++) {
                menuViewModel.MenuItemsView[i].Show();
            }
        }

        private void HideMenuItems()
        {
            for (int i = 0; i < menuViewModel.MenuItemsView.Count; i++) {
                menuViewModel.MenuItemsView[i].Hide();
            }
        }

        private void Bind()
        {
            menuViewModel.OpenedView.Subscribe(SwitchState).AddTo(this);
        }
    }
}
