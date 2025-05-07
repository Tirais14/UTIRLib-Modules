using UniRx;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class StorageViewModel : ViewModel
    {
        [SerializeField] protected StorageModel storageModel = null!;

        public IReadOnlyReactiveProperty<bool> IsOpenedView => storageModel.IsOpenedProp;

        protected override void OnAwake()
        {
            base.OnAwake();
            AssignComponent(ref storageModel);
        }

        protected override void BindToModel()
        { }
    }
}
