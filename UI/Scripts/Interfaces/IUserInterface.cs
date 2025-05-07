using UnityEngine.EventSystems;
using UnityEngine.UI;
using UTIRLib.InputSystem;

#nullable enable
namespace UTIRLib.UI
{
    public interface IUserInterface
    {
        EventSystem EventSystem { get; }
        GraphicRaycaster DefaultRaycaster { get; }
        IPointerHandler PointerHandler { get; }
        IRaycaster Raycaster { get; }
    }
}
