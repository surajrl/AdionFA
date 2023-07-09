using AdionFA.Infrastructure.Common.Base;
using AdionFA.Infrastructure.Common.Directories.Contracts;
using AdionFA.Infrastructure.Common.Managements;
using AdionFA.Infrastructure.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Common.Directories.Services
{
    public class ProjectDirectoryService : InfrastructureServiceBase, IProjectDirectoryService
    {
        public ProjectDirectoryService() : base()
        {
        }

        public bool HasWritePermissionOnPath(string path)
        {
            try
            {
                var writeAllow = false;
                var writeDeny = false;

                var accessControlList = new DirectoryInfo(path).GetAccessControl();
                if (accessControlList == null)
                {
                    return false;
                }

                var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                if (accessRules == null)
                {
                    return false;
                }

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    {
                        continue;
                    }

                    if (rule.AccessControlType == AccessControlType.Allow)
                    {
                        writeAllow = true;
                    }
                    else if (rule.AccessControlType == AccessControlType.Deny)
                    {
                        writeDeny = true;
                    }
                }

                return writeAllow && !writeDeny;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool ExistDefaultWorkspace()
        {
            try
            {
                return Directory.Exists(ProjectDirectoryManager.DefaultDirectory());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool CreateDefaultWorkspace()
        {
            try
            {
                var workspacePath = ProjectDirectoryManager.DefaultDirectory();

                var di = new DirectoryInfo(workspacePath);
                if (!Directory.Exists(workspacePath))
                {
                    di.Create();
                    di.CreateSubdirectory(ProjectDirectoryEnum.Projects.GetDescription());
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool CreateDefaultProjectWorkspace(string projectName)
        {
            try
            {
                if (ExistDefaultWorkspace())
                {
                    var di = new DirectoryInfo(ProjectDirectoryManager.ProjectsDirectoryBase());
                    if (di.Exists)
                    {
                        // Extractor

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectName));

                        // Strategy Builder

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderNodesUP.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderNodesDOWN.GetDescription(), projectName));

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorWithoutSchedule.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Asia.GetMetadata().Name));

                        // Assembly Builder

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription(), projectName));

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(), projectName, "UP"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Asia.GetMetadata().Name));

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(), projectName, "DOWN"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Asia.GetMetadata().Name));

                        // Crossing Builder

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription(), projectName));

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(), projectName, "UP"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Asia.GetMetadata().Name));

                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(), projectName, "DOWN"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Asia.GetMetadata().Name));
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool CopyCSVFileTo(FileInfo fi, string targetDir)
        {
            try
            {
                if (File.Exists(fi.FullName))
                {
                    File.Copy(fi.FullName, Path.Combine(targetDir, fi.Name));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public async Task<bool> CopyCSVFileToAsync(FileInfo fi, string targetDir)
        {
            try
            {
                if (File.Exists(fi.FullName))
                {
                    using var SourceStream = File.Open(fi.FullName, FileMode.Open);
                    using var DestinationStream = File.Create(Path.Combine(targetDir, fi.Name));

                    await SourceStream.CopyToAsync(DestinationStream);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool CopyCSVFiles(string sourceDir, string targetDir)
        {
            try
            {
                if (Directory.Exists(sourceDir) && Directory.Exists(targetDir) && HasWritePermissionOnPath(targetDir))
                {
                    var txtFiles = Directory.EnumerateFiles(sourceDir, "*.csv");
                    foreach (var currentFile in txtFiles)
                    {
                        var fileName = currentFile[(sourceDir.Length + 1)..];
                        File.Copy(currentFile, Path.Combine(targetDir, fileName));
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public FileInfo[] GetFilesInPath(string path, string ext = "*.csv")
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles(ext);
            return files;
        }

        public bool CreateBackup(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new(path);
                    var bu = di.CreateSubdirectory("Backup_" + DateTime.UtcNow.Ticks);
                    return CopyCSVFiles(di.FullName, di.FullName);
                }
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool DeleteFile(string path)
        {
            try
            {
                // Check if file exists with its full path
                if (File.Exists(path))
                {
                    // If file found, delete it
                    File.Delete(path);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public bool DeleteAllFiles(string sourceDir, string ext = "*.csv", SearchOption option = 0, bool overwrite = false, bool isBackup = true)
        {
            var backupDir = @$"{sourceDir}\Backup_" + DateTime.UtcNow.Ticks;

            try
            {
                var csvList = Directory.GetFiles(sourceDir, ext, option).Where(d => !d.Contains("Backup_")).ToArray();

                if (isBackup)
                {
                    // Copy text files
                    foreach (var f in csvList)
                    {
                        // Remove path from the file name
                        var fName = f[(sourceDir.Length + 1)..];

                        try
                        {
                            if (!Directory.Exists(backupDir))
                            {
                                Directory.CreateDirectory(backupDir);
                            }

                            // Will not overwrite if the destination file already exists
                            var targetPath = Path.Combine(backupDir, option == SearchOption.AllDirectories ? Path.GetFileName(fName) : fName);
                            File.Copy(Path.Combine(sourceDir, fName), targetPath, overwrite);
                        }

                        // Catch exception if the file was already copied.
                        catch (IOException copyError)
                        {
                            Trace.TraceError(copyError.Message);
                            throw;
                        }
                    }
                }

                // Delete source files that were copied.
                foreach (var f in csvList)
                {
                    File.Delete(f);
                }

                return true;
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Trace.TraceError(dirNotFound.Message);
                throw;
            }
        }
    }
}