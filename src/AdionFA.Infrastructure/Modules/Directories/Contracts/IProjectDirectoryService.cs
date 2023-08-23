using System.IO;

namespace AdionFA.Infrastructure.Directories.Contracts
{
    public interface IProjectDirectoryService
    {
        bool CreateDefaultWorkspace();

        bool CreateDefaultProjectWorkspace(string projectName);

        bool CopyCSVFileTo(FileInfo fileInfo, string targetDir);

        FileInfo[] GetFilesInPath(string filePath, string fileExtension);

        bool CreateBackup(string filePath);

        bool DeleteFile(string filePath);

        bool DeleteAllFiles(string sourceDir, string fileExtension, bool doBackup);
    }
}
