using System;

namespace UTIRLib.Database
{
    public class DatabaseNotLoadedException : Exception
    {
        public DatabaseNotLoadedException(string databaseName, string message = null) :
           base($"Attempting to use database ({databaseName}) that has not been loaded! {message}")
        { }
    }
}
