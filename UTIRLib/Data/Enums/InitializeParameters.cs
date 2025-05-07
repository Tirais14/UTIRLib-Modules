using System;

namespace UTIRLib.Enums
{
    [Flags]
    public enum InitializeParameters
    {
        ArgumentsNotNull = 2,
        ArgumentsMayBeNull = 4
    }
}
