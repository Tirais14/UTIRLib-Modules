using System;
using UnityEngine;

namespace UTIRLib.UI
{
    [Obsolete]
    public abstract class ModelBase : MonoBehaviour
    {
        protected abstract void BindToViewModel();
    }
}
