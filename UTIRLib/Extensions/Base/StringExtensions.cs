using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class StringExtensions
    {
        /// <summary>
        /// Insert white space between lower case and upper case letters
        /// </summary>
        /// <returns>New or empty string</returns>
        public static string InsertWhiteSpacesBetweenWordsByCase(this string str)
        {
            StringBuilder stringBuilder = new();
            int charsCount = str.Length;
            int lastUpperCaseIdx = -1;
            int lowerCaseLettersCount = 0;
            for (int i = 0; i < charsCount; i++) {
                if (!char.IsLetter(str[i])) {
                    stringBuilder.Append(str[i]);
                    lowerCaseLettersCount = 0;
                    continue;
                }

                if (char.IsUpper(str[i])) {
                    if (lastUpperCaseIdx != -1 && lowerCaseLettersCount >= 1) {
                        stringBuilder.Append(' ');
                        lowerCaseLettersCount = 0;
                    }
                    lastUpperCaseIdx = i;
                }
                else {
                    lowerCaseLettersCount++;
                }
                stringBuilder.Append(str[i]);
            }

            return lastUpperCaseIdx != -1 ? stringBuilder.ToString() : str;
        }

        public static string RemoveChar(this string str, char toRemove) =>
            str.Replace($"{toRemove}", string.Empty);
        public static string RemoveChar(this string str, char toRemove, StringComparison stringComparison) =>
            str.Replace($"{toRemove}", string.Empty, stringComparison);

        public static string RemoveSubstring(this string str, string toRemove) =>
            str.Replace(toRemove, string.Empty);
        public static string RemoveSubstring(this string str, string toRemove,
            System.StringComparison stringComparison) =>
            str.Replace(toRemove, string.Empty, stringComparison);

        public static FileInfo AsFileInfo(this string str) =>
            new(str);

        public static FileInfoExtended AsFileInfoExtended(this string str) =>
            new(str);

        public static DirectoryInfo AsDirectoryInfo(this string str) =>
            new(str);

        public static DirectoryInfoExtended AsDirectoryInfoExtended(this string str) =>
            new(str);

        public static string Format(this string str, bool missingArgsIsNull, params object[] args)
        {
            MatchCollection matches = Regex.Matches(str, @"\{\d+\}");
            if (matches == null || matches.Count == 0) {
                Debug.LogError("Incorrect string to format.");
                return str;
            }

            object[] convertedArgs = new object[matches.Count];
            Array.Copy(args, convertedArgs, args.Length);
            return string.Format(str, convertedArgs);
        }
        public static string Format(this string str, params object[] args) => string.Format(str, args);
        public static string Format(this string str, IFormatProvider provider, params object[] args) =>
            string.Format(provider, str, args);

        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);
    }
}
