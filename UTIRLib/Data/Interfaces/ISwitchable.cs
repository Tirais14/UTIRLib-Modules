using UnityEngine;

namespace UTIRLib
{
    public interface ISwitchable
    {
        public bool IsEnabled { get; }

        public void Enable();

        public void Disable();
    }
}
