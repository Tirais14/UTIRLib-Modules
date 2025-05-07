using Object = UnityEngine.Object;

namespace UTIRLib.Diagnostics
{
    public class AlreadyInitializedMessage : ConsoleMessage
    {
        public AlreadyInitializedMessage() :
            base("Object already initialized.", CallStackFramesOffsetConstructor)
        { }
        public AlreadyInitializedMessage(string objectName) :
            base($"Object {objectName} already initialized.", CallStackFramesOffsetConstructor)
        { }
        public AlreadyInitializedMessage(object obj) :
            base($"Object {(obj is Object unityObj ? unityObj.name : obj.GetType().Name)} already initialized.",
                CallStackFramesOffsetConstructor)
        { }
    }
}
