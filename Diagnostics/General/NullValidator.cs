using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace UTIRLib.Diagnostics
{
    [Obsolete]
    public static class NullValidator
    {
        public static bool ArgumentHasNull([NotNullWhen(true)] out ArgumentNullException? exception, params ArgumentInfo[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++) {
                if (arguments[i].argumentValue == null) {
                    if (arguments[i].argumentName != string.Empty) {
                        exception = new ArgumentNullException(arguments[i].argumentName);
                    }
                    else {
                        exception = new ArgumentNullException();
                    }
                    return true;
                }
            }

            exception = null;
            return false;
        }
    }
}
