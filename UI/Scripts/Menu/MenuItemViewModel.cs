using UniRx;

#nullable enable
namespace UTIRLib.UI.MenuSystem
{
    public class MenuItemViewModel : MonoInitializable<MenuItem>
    {
        protected MenuItem menuItem = null!;

        public IReadOnlyReactiveProperty<bool> IsEnabledView => menuItem.IsEnabledProp;
        public string MenuItemNameView => menuItem.MenuItemName;

        protected override void InitializeInternal(MenuItem menuItem) => this.menuItem = menuItem;
    }
}
