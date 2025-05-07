using UTIRLib.Diagnostics;

namespace UTIRLib.Database
{
    public class DatabaseGroupNotFoundMessage : ConsoleMessage
    {
        public DatabaseGroupNotFoundMessage() :
            base($"Not found database with specified key.", CallStackFramesOffsetConstructor)
        { }
        public DatabaseGroupNotFoundMessage(string databaseKey) :
            base($"Not found database with specified key {databaseKey}.", 
                CallStackFramesOffsetConstructor)
        { }
    }
}