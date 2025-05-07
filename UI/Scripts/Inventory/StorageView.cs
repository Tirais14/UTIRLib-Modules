using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    [RequireComponent(typeof(Image), typeof(StorageModel))]
    public class StorageView : View
    {
        [SerializeField] protected StorageViewModel storageViewModel = null!;
        protected Image image = null!;

        protected void SetImageState(bool state) => image.enabled = state;

        protected void SetGameObjectState(bool state) => gameObject.SetActive(state);

        protected override void BindToViewModel()
        {
            storageViewModel.IsOpenedView.Subscribe(SetImageState).AddTo(this);
            storageViewModel.IsOpenedView.Subscribe(SetGameObjectState).AddTo(this);
        }
    }
}
