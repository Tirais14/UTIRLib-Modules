#nullable enable
namespace UTIRLib
{
    public abstract class MonoInitializerAuto : MonoInitializer
    {
        protected void Awake() => StartCoroutine(IntializeCoroutine());
    }
}
