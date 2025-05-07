using System;
using UnityEngine;
using Object = UnityEngine.Object;

#nullable enable
namespace UTIRLib.Diagnostics.Exceptions
{
    public static class ExceptionHelper
    {
        public static Exception? CreateMessage<T>(T param, string paramName, bool isArg = true)
        {
            Type objType = typeof(T);
            if (objType.IsOrSubclassOf(typeof(Component))) {
                if (param == null) {
                    if (!isArg) {
                        return new ObjectNotFoundException(paramName, objType);
                    }
                    else {
                        return new ArgumentNullException(paramName);
                    }
                }
            }
            else if (objType.IsOrSubclassOf(typeof(Object))) {
                if (param == null) {
                    if (!isArg) {
                        return new ObjectNotFoundException(paramName, objType);
                    }
                    else {
                        return new ArgumentNullException(paramName);
                    }
                }
            }
            else if (objType.IsOrSubclassOf(typeof(string))) {
                if (string.IsNullOrEmpty(param as string)) {
                    return new NullOrEmptyStringException(param as string, paramName);
                }
            }

            return null;
        }
    }
}
