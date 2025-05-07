using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public sealed class OnLoadSceneDestroyLocker : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Destroy(this);
        }
    }
}
