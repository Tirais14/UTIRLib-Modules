using System;

namespace UTIRLib
{
    public static class EnumHelper
    {
#nullable enable
        public static T[] GetValues<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));

        public static int ValuesCount<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;
    }
}
