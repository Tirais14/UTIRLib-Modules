using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UTIRLib.InputSystem;

#nullable enable
namespace UTIRLib.UI
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UserInterface : MonoBehaviourExtended, IUserInterface
    {
        public EventSystem EventSystem { get; protected set; } = null!;
        public GraphicRaycaster DefaultRaycaster { get; protected set; } = null!;
        public IPointerHandler PointerHandler { get; protected set; } = null!;
        public IRaycaster Raycaster { get; protected set; } = null!;
    }
}