using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public class MenuItemView : MonoInitializable<MenuItemViewModel>
    {
        protected MenuItemViewModel menuItemViewModel = null!;
        protected Image image = null!;
        protected Button button = null!;
        protected TextMeshProUGUI textMeshProUGUI = null!;
        protected Transform[] childs = null!;

        protected override void InitializeInternal(MenuItemViewModel menuItemViewModel)
        {
            this.menuItemViewModel = menuItemViewModel;
            AssignComponent(ref image);
            childs = GetChilds();
            Bind();
        }

        private void SetState(bool state)
        {
            image.enabled = state;
            button.enabled = state;
            for (int i = 0; i < childs.Length; i++) {
                childs[i].gameObject.SetActive(state);
            }
        }

        private void Bind()
        {
            textMeshProUGUI.text = menuItemViewModel.MenuItemNameView;
            menuItemViewModel.IsEnabledView.Subscribe(SetState).AddTo(this);
        }
    }
}
