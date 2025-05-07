using System;

namespace UTIRLib.Diagnostics
{
    public class UtirlibException : Exception
    {
        protected UtirlibException(string message) : base(message) 
        { }
        protected UtirlibException(string notFormattedMessage, params object[] args) :
            base(notFormattedMessage.Format(args))
        { }
    }
}
