using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UTIRLib.Diagnostics.Exceptions;
using Zenject;
using System.Reflection;

#nullable enable
#pragma warning disable S2955 // Generic parameters not constrained to reference types should not be compared to "null"
namespace UTIRLib
{
    public static class ObjectHelper
    {
        /// <summary>Checks for unity or system <see langword="null"/></summary>
        public static bool IsNull<T>([NotNullWhen(false)] T? obj) => obj is Object unityObj && unityObj == null ||
            obj == null;

        /// <summary>Checks for unity or system not <see langword="null"/></summary>
        public static bool IsNotNull<T>([NotNullWhen(true)] T? obj) => obj is Object unityObj && unityObj != null &&
            obj != null;
    }
}
