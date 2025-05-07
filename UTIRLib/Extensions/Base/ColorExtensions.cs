using UnityEngine;

namespace UTIRLib
{
    public static class ColorExtensions
    {
        public static Color Set(this Color color, float r, float g, float b) => new(r, g, b, color.a);

        public static Color SetRed(this Color color, float red) => new(red, color.g, color.b, color.a);

        public static Color SetGreen(this Color color, float green) => new(color.r, green, color.b, color.a);

        public static Color SetBlue(this Color color, float blue) => new(color.r, color.g, blue, color.a);

        public static Color SetAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
    }
}
