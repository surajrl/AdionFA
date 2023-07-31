using System.IO;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Directories.Contracts
{
    public interface IProjectDirectoryService
    {
        bool HasWritePermissionOnPath(string path);
        bool ExistDefaultWorkspace();
        bool CreateDefaultWorkspace();
        bool CreateDefaultProjectWorkspace(string projectName);
        bool CopyCSVFileTo(FileInfo fi, string targetDir);
        Task<bool> CopyCSVFileToAsync(FileInfo fi, string targetDir);
        bool CopyCSVFiles(string sourceDir, string targetDir);
        FileInfo[] GetFilesInPath(string path, string ext = "*.csv");
        bool CreateBackup(string path);
        bool DeleteFile(string path);
        bool DeleteAllFiles(string sourceDir, string ext = "*.csv", SearchOption option = 0, bool overwrite = false, bool isBackup = true);
    }
}
