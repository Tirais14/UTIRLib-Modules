using UTIRLib.Diagnostics;

namespace UTIRLib.Database
{
    public class AssetDatabaseNotFoundMessage : ConsoleMessage
    {
        public AssetDatabaseNotFoundMessage() :
            base($"Not found database with key.", CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseNotFoundMessage(string databaseKey) :
            base($"Not found database with key {databaseKey}.", CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseNotFoundMessage(object databaseGroup, string databaseKey) : 
            base($"Not found database int group {databaseGroup.GetType().Name} with key {databaseKey}.",
            CallStackFramesOffsetConstructor)
        { }
    }
}