using UTIRLib.Attributes;

namespace UTIRLib.Enums
{
    public enum AssetTypeName
    {
        None,
        [CustomString("UnityObject")]
        Generic,
        [CustomString("Prefab")]
        GameObject,
        ScriptableObject,
        Scene
    }
}
