using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

#nullable enable
namespace UTIRLib.Diagnostics
{
    public static class MessageHelper
    {
        public static ConsoleMessage? CreateMessage<T>(T param, string paramName, bool isArg = true)
        {
            Type objType = typeof(T);
            if (objType.IsOrSubclassOf(typeof(Component))) {
                if (param == null) {
                    if (!isArg) {
                        return new ObjectNotFoundMessage(paramName, objType);
                    }
                    else {
                        return new ArgumentNullMessage(paramName);
                    }
                }
            }
            else if (objType.IsOrSubclassOf(typeof(Object))) {
                if (param == null) {
                    if (!isArg) {
                        return new ObjectNotFoundMessage(paramName, objType);
                    }
                    else {
                        return new ArgumentNullMessage(paramName);
                    }
                }
            }
            else if (objType.IsOrSubclassOf(typeof(string))) {
                if (string.IsNullOrEmpty(param as string)) {
                    return new NullOrEmptyStringMessage(param as string, paramName);
                }
            }
            else if (objType.IsOrSubclassOf(typeof(IEnumerable))) {
                return new NullOrEmptyCollectionMessage(param, paramName);
            }

            return null;
        }
    }
}
