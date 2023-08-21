using System.IO;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Directories.Contracts
{
    public interface IProjectDirectoryService
    {
        bool HasWritePermissionOnPath(string filePath);

        bool ExistDefaultWorkspace();

        bool CreateDefaultWorkspace();

        bool CreateDefaultProjectWorkspace(string projectName);

        bool CopyCSVFileTo(FileInfo fileInfo, string targetDir);

        Task<bool> CopyCSVFileToAsync(FileInfo fileInfo, string targetDir);

        bool CopyCSVFiles(string sourceDir, string targetDir);

        FileInfo[] GetFilesInPath(string filePath, string fileExtension);

        bool CreateBackup(string filePath);

        bool DeleteFile(string filePath);

        bool DeleteAllFiles(string sourceDir, string fileExtension, bool doBackup);
    }
}
