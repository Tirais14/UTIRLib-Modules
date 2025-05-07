using System.IO;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    /// <summary>
    /// Helper class for handy work with files
    /// </summary>
    public class FileInfoExtended
    {
        private DirectoryInfoExtended? directoryInfo;
        private string directoryPath = null!;
        private string fileName = null!;
        private string extension = null!;
        private string combinedPath = null!;
        private FileInfo? fileInfo;

        public DirectoryInfoExtended? DirectoryInfo => directoryInfo;
        public string DirectoryPath {
            get => directoryPath;
            set => SetDirectoryPath(value);
        }
        public string FileName {
            get => fileName;
            set => SetFileName(value);
        }
        public string Extension {
            get => extension;
            set => SetExtension(value);
        }
        public string FilePath => combinedPath;
        public FileInfo? Base => fileInfo;
        public bool IsEmpty => directoryInfo == null && fileInfo == null;
        public bool Exists => fileInfo != null && fileInfo.Exists;
        public bool HasFileName => fileInfo != null && !string.IsNullOrEmpty(fileInfo.Name);
        public bool HasExtension => fileInfo != null && !string.IsNullOrEmpty(fileInfo.Extension);

        public FileInfoExtended() =>
            ErasePath();
        /// <exception cref="NullOrEmptyStringException"></exception>
        public FileInfoExtended(string path, bool includeFileName = true, bool includeExtension = true)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new NullOrEmptyStringException(nameof(path));
            }

            if (includeFileName) {
                combinedPath = path;
                fileName = Path.GetFileNameWithoutExtension(path);
                if (includeExtension) {
                    extension = Path.GetExtension(path);
                }
                else {
                    extension = string.Empty;
                }
                directoryPath = path.RemoveSubstring(fileName + extension);
            }
            else {
                directoryPath = path;
                combinedPath = string.Empty;
                fileName = string.Empty;
                extension = string.Empty;
            }

            UpdateAndRecombinePath();
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        public FileInfoExtended(string directoryPath, string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath));
            }
            if (string.IsNullOrEmpty(fileNameWithExtension)) {
                throw new NullOrEmptyStringException(nameof(fileNameWithExtension));
            }

            this.directoryPath = directoryPath;
            fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension);
            extension = Path.GetExtension(fileNameWithExtension);
            combinedPath = string.Empty;
            UpdateAndRecombinePath();
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        public FileInfoExtended(string directoryPath, string fileName, string extension)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath));
            }
            if (string.IsNullOrEmpty(fileName)) {
                throw new NullOrEmptyStringException(nameof(fileName));
            }
            if (string.IsNullOrEmpty(extension)) {
                throw new NullOrEmptyStringException(nameof(extension));
            }

            this.directoryPath = directoryPath;
            this.fileName = fileName;
            this.extension = extension;
            combinedPath = string.Empty;
            UpdateAndRecombinePath();
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetDirectoryPath(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath));
            }

            this.directoryPath = directoryPath;
            UpdateAndRecombinePath();
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) {
                throw new NullOrEmptyStringException(nameof(fileName));
            }

            this.fileName = fileName;
            UpdateAndRecombinePath();
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetFileNameWithExtension(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension)) {
                throw new NullOrEmptyStringException(nameof(fileNameWithExtension));
            }

            fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension);
            extension = Path.GetExtension(fileNameWithExtension);
            UpdateAndRecombinePath();
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetFileNameWithExtension(string fileName, string extension)
        {
            if (string.IsNullOrEmpty(fileName)) {
                throw new NullOrEmptyStringException(nameof(fileName));
            }
            if (string.IsNullOrEmpty(extension)) {
                throw new NullOrEmptyStringException(nameof(extension));
            }

            this.fileName = fileName;
            this.extension = extension;
            UpdateAndRecombinePath();
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public void SetExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) {
                throw new NullOrEmptyStringException(nameof(extension));
            }

            this.extension = extension;
            UpdateAndRecombinePath();
        }

        /// <summary>
        /// All string values sets to empty
        /// </summary>
        public void ErasePath()
        {
            directoryInfo = null;
            directoryPath = string.Empty;
            fileName = string.Empty;
            extension = string.Empty;
            combinedPath = string.Empty;
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        public FileInfoExtended ConvertToRelativePathQuery(string relativeTo)
        {
            if (string.IsNullOrEmpty(relativeTo)) {
                throw new NullOrEmptyStringException(nameof(relativeTo));
            }

            directoryPath = Path.GetRelativePath(relativeTo, directoryPath);
            UpdateAndRecombinePath();
            return this;
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        public void ConvertToRelativePath(string relativeTo)
        {
            if (string.IsNullOrEmpty(relativeTo)) {
                throw new NullOrEmptyStringException(nameof(relativeTo));
            }

            directoryPath = Path.GetRelativePath(relativeTo, directoryPath);
            UpdateAndRecombinePath();
        }

        public FileInfoExtended ConvertToWindowsPathQuery()
        {
            directoryPath = FileSystemPathHelper.ToWindowsStyle(directoryPath);
            UpdateAndRecombinePath();
            return this;
        }

        public void ConvertToWindowsPath()
        {
            directoryPath = FileSystemPathHelper.ToWindowsStyle(directoryPath);
            UpdateAndRecombinePath();
        }

        public FileInfoExtended ConvertToUniversalPathQuery()
        {
            directoryPath = FileSystemPathHelper.ToUniversalStyle(directoryPath);
            UpdateAndRecombinePath();
            return this;
        }

        public void ConvertToUniversalPath()
        {
            directoryPath = FileSystemPathHelper.ToUniversalStyle(directoryPath);
            UpdateAndRecombinePath();
        }

        public FileInfoExtended DeepCopy()
        {
            FileInfoExtended fileInfoExtendedCopy = new() {
                directoryInfo = new DirectoryInfoExtended(directoryPath),
                directoryPath = this.directoryPath,
                fileName = this.fileName,
                extension = this.extension,
                combinedPath = this.combinedPath,
                fileInfo = new FileInfo(combinedPath)
            };

            return fileInfoExtendedCopy;
        }

        public override string ToString() => combinedPath;

        public override int GetHashCode() => combinedPath.GetHashCode();

        private static string NormalizeSeparator(string path) =>
            FileSystemPathHelper.ToUniversalStyle(path);

        private void UpdateAndRecombinePath()
        {
            CombinePath();
            Update();
        }

        private void CombinePath()
        {
            if (string.IsNullOrEmpty(fileName)) {
                combinedPath = string.Empty;
                Update();
                return;
            }

            string normalizedDirectoryPath = NormalizeSeparator(directoryPath);
            string fullFileName = fileName + extension;
            combinedPath = Path.Combine(normalizedDirectoryPath, fullFileName);
            Update();
        }

        private void Update()
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                directoryInfo = null;
            }
            else {
                directoryInfo = new DirectoryInfoExtended(directoryPath);
            }

            if (string.IsNullOrEmpty(combinedPath)) {
                fileInfo = null;
            }
            else {
                fileInfo = new FileInfo(combinedPath);
            }
        }
    }
}
