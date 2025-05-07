#nullable enable
namespace UTIRLib
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Same as '!' for better readability
        /// </summary>
        public static bool Inverse(this bool value) => !value;
    }
}
