using System;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    [RequireComponent(typeof(Menu), typeof(MenuViewModel), typeof(MenuView))]
    public class MenuInitializer : MonoInitializerAuto
    {
        protected override void IntiializeObjects()
        {
            var menu = GetComponent<Menu>();
            var menuViewModel = GetComponent<MenuViewModel>();
            var menuView = GetComponent<MenuView>();

            menu.Initialize();
            menuViewModel.Initialize(menu);
            menuView.Initialize(menuViewModel);
        }

        protected override bool IntiializePredicate() => true;
    }
}
