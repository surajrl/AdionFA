using AdionFA.Infrastructure.Common.Directories.Contracts;

namespace AdionFA.Benchmark.Services
{
    public class BenchmarkProjectDirectoryService : IProjectDirectoryService
    {
        public bool CopyCSVFiles(string sourceDir, string targetDir)
        {
            throw new NotImplementedException();
        }

        public bool CopyCSVFileTo(FileInfo fi, string targetDir)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CopyCSVFileToAsync(FileInfo fi, string targetDir)
        {
            throw new NotImplementedException();
        }

        public bool CreateBackup(string path)
        {
            throw new NotImplementedException();
        }

        public bool CreateDefaultProjectWorkspace(string projectName)
        {
            throw new NotImplementedException();
        }

        public bool CreateDefaultWorkspace()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllFiles(string sourceDir, string ext = "*.csv", SearchOption option = SearchOption.TopDirectoryOnly, bool overwrite = false, bool isBackup = true)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool ExistDefaultWorkspace()
        {
            throw new NotImplementedException();
        }

        public FileInfo[] GetFilesInPath(string path, string ext = "*.csv")
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles(ext);
            return files;
        }

        public bool HasWritePermissionOnPath(string path)
        {
            throw new NotImplementedException();
        }
    }
}
