using System;

namespace UTIRLib.Database
{
    public class DatabaseLoadException : Exception
    {
        public DatabaseLoadException(string message) :
           base($"Critical error while loading database! {message}")
        { }
    }
}