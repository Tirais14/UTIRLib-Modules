using System;

#nullable enable
namespace UTIRLib
{
    public static class FloatExtensions
    {
        public static bool EqualsAround(this float a, float b, float epsilon = 0.0001f) => MathF.Abs(a - b) < epsilon;
        public static bool NotEqualsAround(this float a, float b, float epsilon = 0.0001f) => MathF.Abs(a - b) > epsilon;
    }
}
