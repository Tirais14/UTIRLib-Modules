using System;
using System.IO;
using UnityEngine;
using UTIRLib.Diagnostics.Exceptions;
using UTIRLib.Json.Structs;

namespace UTIRLib.Json
{
    public class JsonFile
    {
        protected const string JSON_EXTENSION = ".json";

        protected FileInfoExtended fileInfo;
        protected object data;

        public object Data => data;
        public FileInfoExtended FileInfo {
            get => fileInfo;
            set => SetFilePath(value);
        }
        public string FileName => fileInfo.FileName;
        public string FilePath => fileInfo.FilePath;
        public bool Exists => fileInfo.Base.Exists;

        protected JsonFile() { }
        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public JsonFile(object data, string directoryPath, string fileName)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath)); 
            }
            if (string.IsNullOrEmpty(fileName)) { 
                throw new NullOrEmptyStringException(nameof(fileName)); 
            }

            this.data = data ?? throw new ArgumentNullException(nameof(data));
            fileInfo = new FileInfoExtended(directoryPath, fileName, JSON_EXTENSION);
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public JsonFile(object data, string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) {
                throw new NullOrEmptyStringException(nameof(filePath));
            }

            this.data = data ?? throw new ArgumentNullException(nameof(data));
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            fileInfo = new FileInfoExtended(filePath, fileName, JSON_EXTENSION);
        }
        /// <exception cref="ArgumentNullException"></exception>
        public JsonFile(object data, FileInfoExtended fileInfo)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
            this.fileInfo = fileInfo;

            if (this.fileInfo.Extension != JSON_EXTENSION) {
                fileInfo.Extension = JSON_EXTENSION;
            }
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static JsonFile Open(string filePath, Type type)
        {
            if (string.IsNullOrEmpty(filePath)) {
                throw new NullOrEmptyStringException(nameof(filePath));
            }
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            if (!File.Exists(filePath)) {
                throw new FileNotFoundException(filePath);
            }

            JsonFileDeserializer deserializer = new(){
                toDeserialize = File.ReadAllText(filePath),
                toDeserializeType = type
            };
            deserializer.Deserialize();

            return new JsonFile(deserializer.Deserialized, filePath);
        }
        public static bool TryOpen(string filePath, Type type, out JsonFile jsonFile)
        {
            jsonFile = null;
            try {
                jsonFile = Open(filePath, type);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                throw;
            }

            return jsonFile != null;
        }

        /// <exception cref="Exception"></exception>
        public virtual void Save(bool owerwrite = false)
        {
            if (!fileInfo.HasFileName) {
                throw new Exception("File name cannot be empty.");
            }

            string serialized = Serialize();
            if (Exists && !owerwrite) {
                UnityEngine.Debug.LogWarning($"{nameof(JsonFile)} {fileInfo.FileName} on path: \"{FilePath}\" doesn't created because owerwrite not allowed.");
            }
            else if (Exists && owerwrite) {
                Delete();
            }

            DirectoryInfo targetDirectory = Directory.GetParent(FilePath);
            if (!targetDirectory.Exists) {
                Directory.CreateDirectory(targetDirectory.FullName);
            }

            File.WriteAllText(FilePath, serialized);
        }

        public virtual void SaveAs(string filePath, bool owerwrite = false)
        {
            SetFilePath(filePath);
            Save(owerwrite);
        }
        public virtual void SaveAs(FileInfoExtended pathInfo, bool owerwrite = false)
        {
            fileInfo = pathInfo;
            Save(owerwrite);
        }

        public void Delete() => fileInfo.Base.Delete();

        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) {
                throw new NullOrEmptyStringException(nameof(filePath));
            }

            fileInfo.DirectoryPath = filePath;
            if (fileInfo.Extension != JSON_EXTENSION) {
                fileInfo.Extension = JSON_EXTENSION;
            }
        }
        public void SetFilePath(FileInfoExtended fileInfo)
        {
            this.fileInfo = fileInfo ??
                throw new ArgumentNullException(nameof(fileInfo));

            if (this.fileInfo.Extension != JSON_EXTENSION) {
                this.fileInfo.Extension = JSON_EXTENSION;
            }
        }

        protected virtual string Serialize()
        {
            JsonFileSerializer serializer = new(){
                obj = data,
                formatText = true,
            };
            serializer.Serialize();

            return serializer.Serialized;
        }
    }
    public class JsonFile<T> : JsonFile
    {
        protected new T data;
        public new T Data => data;

        public JsonFile(T data, string directoryPath, string fileName) :
            base(data, directoryPath, fileName)
        { this.data = data; }
        public JsonFile(T data, string filePath) :
            base(data, filePath)
        { this.data = data; }
        public JsonFile(T data, FileInfoExtended fileInfoX) :
            base(data, fileInfoX)
        { this.data = data; }

        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static JsonFile<T> Open(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) {
                throw new NullOrEmptyStringException(nameof(filePath));
            }

            if (!File.Exists(filePath)) {
                throw new FileNotFoundException(filePath);
            }

            JsonFileDeserializer<T> deserializer = new(){
                toDeserialize = File.ReadAllText(filePath),
            };

            return new JsonFile<T>(deserializer.Deserialized, filePath);
        }
        public static bool TryOpen(string filePath, out JsonFile<T> jsonFile)
        {
            jsonFile = null;
            try {
                jsonFile = Open(filePath);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                throw;
            }

            return jsonFile != null;
        }
    }
}
