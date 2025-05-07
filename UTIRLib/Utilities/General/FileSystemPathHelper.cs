using System.IO;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    public static class FileSystemPathHelper
    {
        private const char StraightSlashSeparator = '/';
        private const char BackslashSeparator = '\\';

        /// <summary>
        /// Check for "\\" separator symbols
        /// </summary>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static bool WindowsStyled(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            return path.Contains(BackslashSeparator) && !path.Contains(StraightSlashSeparator);
        }

        /// <summary>
        /// Check for "/" separator symbols
        /// </summary>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static bool UniversalStyled(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            return path.Contains(StraightSlashSeparator) && !path.Contains(BackslashSeparator);
        }

        /// <summary>
        /// Replace "/" symbols to "\\"
        /// </summary>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string ToWindowsStyle(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Replace "\\" symbols to "//"
        /// </summary>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string ToUniversalStyle(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        /// <param name="useUniversalStyle">if true converts path to "/" separator</param>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string AddSeparatorToEnd(string path, bool useUniversalStyle = true)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            string result;
            if (useUniversalStyle) {
                result = ToUniversalStyle(path);
                return result.EndsWith(StraightSlashSeparator) ?
                    path : path + StraightSlashSeparator;
            }

            result = ToWindowsStyle(path);
            return result.EndsWith(BackslashSeparator) ?
                path : path + BackslashSeparator;
        }
    }
}
