using System;
using UTIRLib.Diagnostics;

namespace UTIRLib.Database
{
    public class DatabaseNotFoundMessage : ConsoleMessage
    {
        public DatabaseNotFoundMessage() :
            base("Database not found.", CallStackFramesOffsetConstructor)
        { }
        public DatabaseNotFoundMessage(string databaseKey) :
            base($"Database {databaseKey} not found.", CallStackFramesOffsetConstructor)
        { }
        public DatabaseNotFoundMessage(string databaseKey, Type databaseType) :
            base($"Database {databaseKey} ({databaseType.Name}) not found.",
                CallStackFramesOffsetConstructor)
        { }
    }
}