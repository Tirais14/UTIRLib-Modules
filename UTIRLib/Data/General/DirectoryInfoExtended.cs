using System;
using System.IO;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    /// <summary>
    /// Helper class for handy work with directory
    /// </summary>
    public class DirectoryInfoExtended
    {
        private string? directoryPath;
        private DirectoryInfo? directoryInfo;

        public string DirectoryPath {
            get => directoryPath ?? string.Empty;
            set => SetDriectoryPath(value);
        }
        public string DirectoryName {
            get => directoryInfo?.Name ?? string.Empty;
            set => SetDirectoryName(value);
        }
        public DirectoryInfo? Base => directoryInfo;
        public bool IsEmpty => string.IsNullOrEmpty(directoryPath);
        public bool Exists => directoryInfo?.Exists ?? false;

        public DirectoryInfoExtended() =>
            ErasePath();
        public DirectoryInfoExtended(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath));
            }

            this.directoryPath = directoryPath;
            Update();
        }

        public void SetDriectoryPath(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                throw new NullOrEmptyStringException(nameof(directoryPath));
            }

            this.directoryPath = directoryPath;
            Update();
        }

        public void SetDirectoryName(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName)) {
                throw new NullOrEmptyStringException(nameof(directoryName));
            }

            directoryPath = directoryPath?.RemoveSubstring(DirectoryName);
            Path.Combine(directoryPath);
            Update();
        }

        public void AddToPath(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            directoryPath = Path.Combine(directoryPath, path);
            Update();
        }

        public FileInfoExtended AsFileInfo() =>
            new(directoryPath ?? string.Empty, includeFileName: false, includeExtension: false);
        public FileInfoExtended AsFileInfo(string fileNameWithExtension) =>
            new(directoryPath ?? string.Empty, fileNameWithExtension);
         public FileInfoExtended AsFileInfo(string fileName, string extension) =>
            new(directoryPath ?? string.Empty, fileName, extension);

        /// <summary>
        /// All string values sets to empty
        /// </summary>
        public void ErasePath()
        {
            directoryPath = string.Empty;
            directoryInfo = null;
        }

        #region Shortcuts
        public FileInfo[] GetFiles() => directoryInfo?.GetFiles() ?? Array.Empty<FileInfo>();
        public FileInfo[] GetFiles(string searchPattern) => directoryInfo?.GetFiles(searchPattern) ?? Array.Empty<FileInfo>();
        public FileInfo[] GetFiles(string searchPattern, EnumerationOptions enumerationOptions) =>
            directoryInfo?.GetFiles(searchPattern, enumerationOptions) ?? Array.Empty<FileInfo>();
        public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption) =>
            directoryInfo?.GetFiles(searchPattern, searchOption) ?? Array.Empty<FileInfo>();

        public FileInfo[] GetAllFiles(string? searchPattern = null) =>
            directoryInfo?.GetFiles(searchPattern ?? "*", SearchOption.AllDirectories) ?? Array.Empty<FileInfo>();

        public DirectoryInfo[] GetDirectories() => directoryInfo?.GetDirectories() ?? Array.Empty<DirectoryInfo>();
        public DirectoryInfo[] GetDirectories(string searchPattern) => 
            directoryInfo?.GetDirectories(searchPattern) ?? Array.Empty<DirectoryInfo>();
        public DirectoryInfo[] GetDirectories(string searchPattern,
            EnumerationOptions enumerationOptions) =>
            directoryInfo?.GetDirectories(searchPattern, enumerationOptions) ?? Array.Empty<DirectoryInfo>();
        public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption) =>
            directoryInfo?.GetDirectories(searchPattern, searchOption) ?? Array.Empty<DirectoryInfo>();

        public DirectoryInfo[] GetAllDirectories(string? searchPattern = null) =>
            directoryInfo?.GetDirectories(searchPattern ?? "*", SearchOption.AllDirectories) ?? Array.Empty<DirectoryInfo>();

        public string[] GetFilePaths() =>
            ConvertFileInfosToPaths(directoryInfo?.GetFiles());
        public string[] GetFilePaths(string searchPattern) =>
            ConvertFileInfosToPaths(directoryInfo?.GetFiles(searchPattern));
        public string[] GetFilePaths(string searchPattern,
            EnumerationOptions enumerationOptions) =>
            ConvertFileInfosToPaths(directoryInfo?.GetFiles(searchPattern, enumerationOptions));
        public string[] GetFilePaths(string searchPattern, SearchOption searchOption) =>
            ConvertFileInfosToPaths(directoryInfo?.GetFiles(searchPattern, searchOption));

        public string[] GetAllFilePaths(string? searchPattern = null) =>
            ConvertFileInfosToPaths(directoryInfo?.GetFiles(searchPattern ?? "*", SearchOption.AllDirectories));

        public string[] GetDirectoryPaths() =>
            ConvertDirectoryInfosToPaths(directoryInfo?.GetDirectories());
        public string[] GetDirectoryPaths(string searchPattern) =>
            ConvertDirectoryInfosToPaths(directoryInfo?.GetDirectories(searchPattern));
        public string[] GetDirectoryPaths(string searchPattern,
            EnumerationOptions enumerationOptions) =>
            ConvertDirectoryInfosToPaths(directoryInfo?.GetDirectories(searchPattern, enumerationOptions));
        public string[] GetDirectoryPaths(string searchPattern, SearchOption searchOption) =>
            ConvertDirectoryInfosToPaths(directoryInfo?.GetDirectories(searchPattern, searchOption));

        public string[] GetAllDirectoryPaths(string? searchPattern = null) =>
            ConvertDirectoryInfosToPaths(
                directoryInfo?.GetDirectories(searchPattern ?? "*", SearchOption.AllDirectories));

        public DirectoryInfoExtended[] GetDirectoriesExtended() =>
            ConvertDirectoryInfosToExtended(directoryInfo?.GetDirectories());
        public DirectoryInfoExtended[] GetDirectoriesExtended(string searchPattern) =>
            ConvertDirectoryInfosToExtended(directoryInfo?.GetDirectories(searchPattern));
        public DirectoryInfoExtended[] GetDirectoriesExtended(string searchPattern,
            EnumerationOptions enumerationOptions) =>
            ConvertDirectoryInfosToExtended(directoryInfo?.GetDirectories(searchPattern, enumerationOptions));
        public DirectoryInfoExtended[] GetDirectoriesExtended(string searchPattern, SearchOption searchOption) =>
            ConvertDirectoryInfosToExtended(directoryInfo?.GetDirectories(searchPattern, searchOption));

        public DirectoryInfoExtended[] GetAllDirectoriesExtended(string? searchPattern = null) =>
            ConvertDirectoryInfosToExtended(
                directoryInfo?.GetDirectories(searchPattern ?? "*", SearchOption.AllDirectories));

        public FileInfoExtended[] GetFilesExtended() =>
            ConvertFileInfosToExtended(directoryInfo?.GetFiles());
        public FileInfoExtended[] GetFilesExtended(string searchPattern) =>
            ConvertFileInfosToExtended(directoryInfo?.GetFiles(searchPattern));
        public FileInfoExtended[] GetFilesExtended(string searchPattern,
            EnumerationOptions enumerationOptions) =>
            ConvertFileInfosToExtended(directoryInfo?.GetFiles(searchPattern, enumerationOptions));
        public FileInfoExtended[] GetFilesExtended(string searchPattern, SearchOption searchOption) =>
            ConvertFileInfosToExtended(directoryInfo?.GetFiles(searchPattern, searchOption));

        public FileInfoExtended[] GetAllFilesExtended(string? searchPattern = null) =>
            ConvertFileInfosToExtended(directoryInfo?.GetFiles(searchPattern ?? "*", SearchOption.AllDirectories));
        #endregion

        /// <exception cref="NullOrEmptyStringException"></exception>
        public DirectoryInfoExtended ConvertToRelativePathQuery(string relativeTo)
        {
            if (string.IsNullOrEmpty(relativeTo)) {
                throw new NullOrEmptyStringException(nameof(relativeTo));
            }

            directoryPath = Path.GetRelativePath(relativeTo, directoryPath);
            Update();
            return this;
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        public void ConvertToRelativePath(string relativeTo)
        {
            if (string.IsNullOrEmpty(relativeTo)) {
                throw new NullOrEmptyStringException(nameof(relativeTo));
            }

            directoryPath = Path.GetRelativePath(relativeTo, directoryPath);
            Update();
        }

        public DirectoryInfoExtended ConvertToWindowsPathQuery()
        {
            directoryPath = FileSystemPathHelper.ToWindowsStyle(directoryPath ?? string.Empty);
            Update();
            return this;
        }

        public void ConvertToWindowsPath()
        {
            directoryPath = FileSystemPathHelper.ToWindowsStyle(directoryPath ?? string.Empty);
            Update();
        }

        public DirectoryInfoExtended ConvertToUniversalPathQuery()
        {
            directoryPath = FileSystemPathHelper.ToUniversalStyle(directoryPath ?? string.Empty);
            Update();
            return this;
        }

        public void ConvertToUniversalPath()
        {
            directoryPath = FileSystemPathHelper.ToUniversalStyle(directoryPath ?? string.Empty);
            Update();
        }

        public DirectoryInfoExtended DeepCopy()
        {
            DirectoryInfoExtended directoryInfoExtendedCopy = new() {
                directoryPath = directoryPath,
                directoryInfo = new DirectoryInfo(directoryPath)
            };

            return directoryInfoExtendedCopy;
        }

        public override string ToString() => directoryInfo?.ToString() ?? string.Empty;

        public override int GetHashCode() => directoryInfo?.GetHashCode() ?? 0;

        private static DirectoryInfoExtended[] ConvertDirectoryInfosToExtended(DirectoryInfo[]? directories)
        {
            if (directories.IsNullOrEmpty()) {
                return Array.Empty<DirectoryInfoExtended>();
            }

            int directoriesCount = directories.Length;
            var directoriesExtended = new DirectoryInfoExtended[directoriesCount];

            for (int i = 0; i < directoriesCount; i++) {
                directoriesExtended[i] = new DirectoryInfoExtended(directories[i].FullName);
            }

            return directoriesExtended;
        }
        private static string[] ConvertDirectoryInfosToPaths(DirectoryInfo[]? directories)
        {
            if (directories.IsNullOrEmpty()) {
                return Array.Empty<string>();
            }

            int directoriesCount = directories.Length;
            string[] directoryPaths = new string[directoriesCount];

            for (int i = 0; i < directoriesCount; i++) {
                directoryPaths[i] = directories[i].FullName;
            }

            return directoryPaths;
        }
        private static FileInfoExtended[] ConvertFileInfosToExtended(FileInfo[]? files)
        {
            if (files.IsNullOrEmpty()) {
                return Array.Empty<FileInfoExtended>();
            }

            int filesCount = files.Length;
            var filesExtended = new FileInfoExtended[filesCount];

            for (int i = 0; i < filesCount; i++) {
                filesExtended[i] = new FileInfoExtended(files[i].FullName);
            }

            return filesExtended;
        }
        private static string[] ConvertFileInfosToPaths(FileInfo[]? files)
        {
            if (files.IsNullOrEmpty()) {
                return Array.Empty<string>();
            }

            int filesCount = files.Length;
            string[] filePaths = new string[filesCount];

            for (int i = 0; i < filesCount; i++) {
                filePaths[i] = files[i].FullName;
            }

            return filePaths;
        }

        private void Update()
        {
            if (string.IsNullOrEmpty(directoryPath)) {
                directoryInfo = null;
            }
            else {
                directoryInfo = new DirectoryInfo(directoryPath);
            }
        }

        public static DirectoryInfoExtended operator +(DirectoryInfoExtended a, DirectoryInfoExtended b)
        {
            DirectoryInfoExtended directoryInfo = new(a.directoryPath ?? string.Empty);
            directoryInfo.AddToPath(b.directoryPath ?? string.Empty);
            return directoryInfo;
        }
        public static DirectoryInfoExtended operator +(DirectoryInfoExtended a, string b)
        {
            DirectoryInfoExtended directoryInfo = new(a.directoryPath ?? string.Empty);
            directoryInfo.AddToPath(b);
            return directoryInfo;
        }
    }
}
