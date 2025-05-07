using UnityEngine;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    [RequireComponent(typeof(MenuItem), typeof(MenuItemViewModel), typeof(MenuItemView))]
    public class MenuItemInitializer : MonoInitializerAuto
    {
        protected override void IntiializeObjects()
        {
            var menuItem = GetComponent<MenuItem>();
            var menuItemViewModel = GetComponent<MenuItemViewModel>();
            var menuItemView = GetComponent<MenuItemView>();

            menuItem.Initialize();
            menuItemViewModel.Initialize(menuItem);
            menuItemView.Initialize(menuItemViewModel);
        }

        protected override bool IntiializePredicate() => true;
    }
}
