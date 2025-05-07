using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    public static class CSharpFileHelper
    {
        public const string NAMESPACE_KEYWORD = "namespace";
        public const string USING_KEYWORD = "using";
        public const char START_BRACKET = '{';
        public const char END_BRACKET = '}';

        public enum AccessKeyword
        {
            @public,
            @protected,
            @private,
            @internal
        }

        public enum InstanceTypeKeyword
        {
            none,
            @static,
            @const,
            @record
        }

        public enum DataTypeKeyword
        {
            @class,
            @struct,
            @enum
        }

        public enum EnumInheritType
        {
            @byte,
            @sbyte,
            @short,
            @ushort,
            @int,
            @uint,
            @long,
            @ulong,
        }

        //public enum PropertySetting
        //{
        //    lambdaGetter,
        //    Auto,
        //    LambdaGetter,
        //    LambdaSetter,
        //}

        /// <exception cref="NullOrEmptyStringException"></exception>
        public static void CreateCSharpScriptFile(string directoryPath, string fileName,
            string content, bool overwrite = false, bool formatContent = true, params string[] namespaceParts)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(directoryPath, nameof(directoryPath));
            }
            if (string.IsNullOrEmpty(fileName)) {
                throw new NullOrEmptyStringException(fileName, nameof(fileName));
            }
            if (string.IsNullOrEmpty(content)) {
                throw new NullOrEmptyStringException(content, nameof(content));
            }

            if (namespaceParts != null && namespaceParts.Length > 0 &&
                !namespaceParts.All((namespacePart) => string.IsNullOrEmpty(namespacePart))) {
                SetNamespace(ref content, namespaceParts);
            }

            if (formatContent) {
                content = FormatContent(content);
            }

            FileInfo fileInfo = new(Path.Combine(directoryPath, fileName + ".cs"));
            if (fileInfo.Exists && overwrite || !fileInfo.Exists) {
                File.WriteAllText(fileInfo.FullName, content);
            }
        }

        /// <summary>Wrap content in specified namespace</summary>
        /// <returns>formatted content in one <see cref="string"/></returns>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static void SetNamespace(ref string content, params string[] namespaceParts)
        {
            if (namespaceParts == null) {
                throw new NullElementInCollectionException(nameof(namespaceParts));
            }
            if (namespaceParts.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(namespaceParts, nameof(namespaceParts));
            }
            if (string.IsNullOrEmpty(content)) {
                throw new NullOrEmptyStringException(content, nameof(content));
            }

            if (HasNamesapce(content)) {
                ChangeNamespace(ref content, namespaceParts);
                return;
            }

            StringComparison comparison = StringComparison.CurrentCulture;
            bool namespaceLineAdded = false, usingsAdded = false;
            string[] contentLines = content.Split('\n');
            List<string> namespacedContentLines = new();
            foreach (string line in contentLines) {
                if (line.Contains(USING_KEYWORD, comparison)) {
                    namespacedContentLines.Add(line);
                    usingsAdded = true;
                    continue;
                }

                if (!namespaceLineAdded) {
                    if (IsDataTypeLine(line)) {
                        if (usingsAdded) {
                            namespacedContentLines.Add(string.Empty);
                        }
                        namespacedContentLines.Add($"{NAMESPACE_KEYWORD} {string.Join('.', namespaceParts)}");
                        namespacedContentLines.Add("{");
                        namespacedContentLines.Add(line);
                    }
                    else {
                        namespacedContentLines.Add(line);
                        namespacedContentLines.Add($"{NAMESPACE_KEYWORD} {string.Join('.', namespaceParts)}");
                        namespacedContentLines.Add("{");
                    }

                    namespaceLineAdded = true;
                    continue;
                }

                namespacedContentLines.Add(line);
            }

            namespacedContentLines.Add("}");
            content = string.Join("\n", namespacedContentLines);
        }

        /// <summary>Generate CSharp data type</summary>
        /// <returns>formatted content in one <see cref="string"/></returns>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string GenerateDataType(AccessKeyword accessKeyword,
            InstanceTypeKeyword instanceTypeKeyword, DataTypeKeyword dataTypeKeyword,
            string dataTypeName, string? content = null, bool isReadonly = false)
        {
            if (string.IsNullOrEmpty(dataTypeName)) {
                throw new NullOrEmptyStringException(dataTypeName, nameof(dataTypeName));
            }

            List<string> dataTypeLines = new();
            string[] contentLines = content?.Split('\n') ?? Array.Empty<string>();
            dataTypeLines.Add($"{accessKeyword} " +
                $"{(instanceTypeKeyword == InstanceTypeKeyword.none ? string.Empty : instanceTypeKeyword)} " +
                $"{(isReadonly ? "readonly " : string.Empty)}{dataTypeKeyword} {dataTypeName}");
            dataTypeLines.Add("{");
            if (contentLines != null && contentLines.Length > 0) {
                dataTypeLines.AddRange(contentLines);
            }
            dataTypeLines.Add("}");

            return string.Join("\n", dataTypeLines);
        }

        /// <summary>Generate CSharp field</summary>
        /// <returns>formatted content in one <see cref="string"/></returns>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string GenerateField(AccessKeyword accessKeyword, InstanceTypeKeyword instanceTypeKeyword,
            string dataTypeKeyword, string fieldName, string? content = null, bool isReadonly = false)
        {
            if (string.IsNullOrEmpty(dataTypeKeyword)) {
                throw new NullOrEmptyStringException(dataTypeKeyword, nameof(dataTypeKeyword));
            }
            if (string.IsNullOrEmpty(fieldName)) {
                throw new NullOrEmptyStringException(fieldName, nameof(fieldName));
            }

            if (dataTypeKeyword == "string") {
                content = $"\"{content}\"";
            }

            return $"{accessKeyword} " +
            $"{(instanceTypeKeyword == InstanceTypeKeyword.none ? string.Empty : instanceTypeKeyword)} " +
            $"{(isReadonly ? "readonly " : string.Empty)}" +
            $"{dataTypeKeyword} {fieldName}{(string.IsNullOrEmpty(content) ? ';' : $" = {content};")}";
        }

        public static string GenerateEnum(AccessKeyword accessKeyword, string enumTypeName, 
            EnumInheritType enumInheritType, bool isFlags = false, 
            params string[] enumNames)
        {
            if (enumNames.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(enumNames, nameof(enumNames));
            }
            if (string.IsNullOrEmpty(enumTypeName)) {
                throw new NullOrEmptyStringException(enumTypeName, nameof(enumTypeName));
            }

            List<string> enumTypeLines = new();
            if (isFlags) {
                enumTypeLines.Add("[Flags]");
            }

            if (enumInheritType != EnumInheritType.@int) {
                enumTypeLines.Add($"{accessKeyword} {DataTypeKeyword.@enum} {enumTypeName} : {enumInheritType}");
            }
            else {
                enumTypeLines.Add($"{accessKeyword} {DataTypeKeyword.@enum} {enumTypeName}");
            }

            enumTypeLines.Add("{");
            int flagValue = 1;
            foreach (string enumName in enumNames) {
                if (string.IsNullOrEmpty(enumName)) continue;

                if (isFlags) {
                    flagValue *= 2;
                    enumTypeLines.Add($"{enumName} = {flagValue},");
                }
                else {
                    enumTypeLines.Add(enumName + ',');
                }
            }
            enumTypeLines.Add("}");

            return string.Join('\n', enumTypeLines);
        }

        public static string GenerateEnum(AccessKeyword accessKeyword, string enumTypeName, bool isFlags = false,
           params string[] enumNames) =>
            GenerateEnum(accessKeyword, enumTypeName, EnumInheritType.@int, isFlags, enumNames);

        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string FormatContent(string content)
        {
            if (string.IsNullOrEmpty(content)) {
                throw new NullOrEmptyStringException(content, nameof(content));
            }

            content = content.RemoveChar('\t');
            string[] contentLines = content.Split('\n');
            string contentLine;
            string tabs = string.Empty;
            bool addTabOnNextStep = false;
            int nestingLevel = 0;
            int contentLinesCount = contentLines.Length;
            for (int i = 0; i < contentLinesCount; i++) {
                contentLine = contentLines[i];

                if (addTabOnNextStep) {
                    tabs += '\t';
                    addTabOnNextStep = false;
                }

                if (contentLine == "{") {
                    nestingLevel++;
                    addTabOnNextStep = true;
                }
                else if (contentLine.Contains('{')) {
                    tabs += '\t';
                    nestingLevel++;
                    addTabOnNextStep = false;
                }
                else if (contentLine == "}" && nestingLevel > 0) {
                    tabs = tabs[0..^1];
                    nestingLevel--;
                }

                contentLines[i] = tabs + contentLine;
            }

            return string.Join("\n", contentLines);
        }

        public static bool HasNamesapce(string content)
        {
            if (string.IsNullOrEmpty(content)) {
                throw new NullOrEmptyStringException(content, nameof(content));
            }

            string[] contentLines = content.Split('\n');
            foreach (string line in contentLines) {
                if (IsNamespaceLine(line)) {
                    return true;
                }
            }

            return false;
        }

        private static void ChangeNamespace(ref string content, params string[] namespaceParts)
        {
            string[] scriptContentLines = content.Split('\n');
            for (int i = 0; i < scriptContentLines.Length; i++) {
                if (scriptContentLines[i].Contains("namespace", StringComparison.CurrentCulture)) {
                    scriptContentLines[i] = $"namespace {string.Join('.', namespaceParts)}";
                    content = string.Join("", scriptContentLines);
                }
            }

            content = string.Join('\n', scriptContentLines);
        }
        private static bool IsNamespaceLine(string line) =>
            line.Contains(NAMESPACE_KEYWORD, StringComparison.CurrentCulture);

        private static bool IsDataTypeLine(string line) =>
            line.Contains(DataTypeKeyword.@class.ToString(), StringComparison.CurrentCulture) ||
            line.Contains(DataTypeKeyword.@enum.ToString(), StringComparison.CurrentCulture) ||
            line.Contains(DataTypeKeyword.@struct.ToString(), StringComparison.CurrentCulture);

        ///// <summary>Generate CSharp property</summary>
        ///// <returns>formatted content in one <see cref="string"/></returns>
        ///// <exception cref="NullOrEmptyStringException"></exception>
        //public static string GenerateProperty(AccessKeyword accessKeyword,
        //    InstanceTypeKeyword instanceTypeKeyword, DataTypeKeyword dataTypeKeyword, string propertyName,
        //    PropertySetting propertySetting = PropertySetting.lambdaGetter,
        //    AccessKeyword getterAccessKeyword = AccessKeyword.@public,
        //    AccessKeyword setterAccessKeyword = AccessKeyword.@protected, string content = null,
        //    bool getterReadonly = false, bool isReadonly = false)
        //{
        //    if (string.IsNullOrEmpty(propertyName)) {
        //        throw new NullOrEmptyStringException(propertyName, nameof(propertyName));
        //    }

        //    string propertyAccessor = propertySetting switch {
        //        PropertySetting.lambdaGetter =>
        //        $" => {propertyName = char.ToLower(propertyName[0]) + propertyName[1..^1]}",
        //        PropertySetting.Auto => $"{{ {(getterReadonly ? "readonly" : string.Empty)} " +
        //        $"{getterAccessKeyword} get; {setterAccessKeyword} set; }}",
        //        PropertySetting.LambdaGetter => throw new NotImplementedException(),
        //        PropertySetting.LambdaSetter => throw new NotImplementedException(),
        //        _ => throw new NotImplementedException()
        //    };

        //    return $"{accessKeyword} " +
        //    $"{(instanceTypeKeyword == InstanceTypeKeyword.none ? string.Empty : instanceTypeKeyword)} " +
        //    $"{(isReadonly ? "readonly" : string.Empty)} " +
        //    $"{dataTypeKeyword} {propertyName} {propertyAccessor} {(string.IsNullOrEmpty(content) ? ';' : $" = {content};")}";
        //}
    }
}
