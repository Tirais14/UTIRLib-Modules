using System;
using System.Text;
using UnityEngine;
using Unnamed2DTopDownGame.Constants;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib
{
    public static class NameBuilder
    {
        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="EmptyCollectionMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string BuildName(params string[] nameParts)
        {
            if (nameParts.IsNullOrEmpty()) {
                Debug.LogError(new NullOrEmptyCollectionMessage(nameParts, nameof(nameParts)));
                return string.Empty;
            }

            return string.Join(ServiceConstants.WORD_SEPARTOR, nameParts);
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// <br/><see cref="ArgumentWrongMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ExtractPrefix(string name)
        {
            if (string.IsNullOrEmpty(name)) {
                Debug.LogError(new NullOrEmptyStringMessage(name, nameof(name)));
                return string.Empty;
            }
            char wordSeparator = ServiceConstants.WORD_SEPARTOR;
            if (!name.Contains(wordSeparator)) {
                Debug.LogError(new ArgumentWrongMessage(name, nameof(name), $"Argument should be contain at least one {wordSeparator} word separator."));
                return string.Empty;
            }

            StringBuilder prefixBuilder = new();
            foreach (char symbol in name) {
                if (symbol != wordSeparator) {
                    prefixBuilder.Append(symbol);
                }
                else break;
            }

            return prefixBuilder.Length > 0 ? prefixBuilder.ToString() : string.Empty;
        }

        public static string RemovePrefix(string name) => name.RemoveSubstring(ExtractPrefix(name));

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// <br/><see cref="ArgumentWrongMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ExtractPostfix(string name)
        {
            if (string.IsNullOrEmpty(name)) {
                Debug.LogError(new NullOrEmptyStringMessage(name, nameof(name)));
                return string.Empty;
            }
            char wordSeparator = ServiceConstants.WORD_SEPARTOR;
            if (!name.Contains(wordSeparator)) {
                Debug.LogError(new ArgumentWrongMessage(name, nameof(name), $"Argument should be contain at least one {wordSeparator} word separator."));
                return string.Empty;
            }

            StringBuilder prefixBuilder = new();
            int nameCharsCount = name.Length;
            char symbol;
            for (int i = nameCharsCount; i >= 0; i--) {
                symbol = name[i];
                if (symbol != wordSeparator) {
                    prefixBuilder.Append(symbol);
                }
                else break;
            }

            if (prefixBuilder.Length > 0) {
                char[] result = prefixBuilder.ToString().ToCharArray();
                Array.Reverse(result);
                return result.ToString();
            }
            else return string.Empty;
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string RemovePostfix(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
                return string.Empty;
            }

            return str.RemoveSubstring(ExtractPostfix(str));
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// <br/><see cref="ArgumentWrongMessage"/>
        /// <br/><see cref="OutOfRangeMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ExtractMainWords(string name, int toExtractWordsCount = 1)
        {
            if (string.IsNullOrEmpty(name)) {
                Debug.LogError(new NullOrEmptyStringMessage(name, nameof(name)));
                return string.Empty;
            }
            char wordSeparator = ServiceConstants.WORD_SEPARTOR;
            if (!name.Contains(wordSeparator)) {
                Debug.LogError(new ArgumentWrongMessage(name, nameof(name), $"Argument should be contain at least one {wordSeparator} word separator."));
                return string.Empty;
            }
            if (toExtractWordsCount < 1) {
                Debug.LogError(new OutOfRangeMessage(toExtractWordsCount, nameof(toExtractWordsCount), 1, OutOfRangeMessage.LimiterType.Min));
                return string.Empty;
            }

            StringBuilder wordsBuilder = new();
            bool wordSeparatorFound = false;
            int wordsFoundCount = 0;
            foreach (char symbol in name) {
                if (!wordSeparatorFound && symbol == wordSeparator) {
                    wordSeparatorFound = true;
                    continue;
                }
                else if (!wordSeparatorFound && symbol != wordSeparator) {
                    continue;
                }

                if (wordSeparatorFound && wordsFoundCount < toExtractWordsCount && symbol != wordSeparator) {
                    wordsBuilder.Append(symbol);
                }
                else if (wordSeparatorFound && symbol == wordSeparator && wordsBuilder.Length > 0) {
                    wordsFoundCount++;
                }
                else if (wordSeparatorFound && wordsFoundCount == toExtractWordsCount) {
                    break;
                }
            }

            return wordsBuilder.Length > 0 ? wordsBuilder.ToString() : string.Empty;
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ConvertToPrefix(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
                return string.Empty;
            }

            return str + ServiceConstants.WORD_SEPARTOR;
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ConvertToPostfix(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
                return string.Empty;
            }

            return ServiceConstants.WORD_SEPARTOR + str;
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="NullOrEmptyStringMessage"/>
        /// </summary>
        /// <returns><see cref="string"/>.Empty if failed</returns>
        public static string ConvertToConstantName(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
                return string.Empty;
            }

            return str.InsertWhiteSpacesBetweenWordsByCase().Replace(' ', '_').ToUpper();
        }
    }
}
