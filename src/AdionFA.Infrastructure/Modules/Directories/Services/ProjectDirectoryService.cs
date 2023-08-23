using AdionFA.Domain.Enums;
using AdionFA.Domain.Extensions;
using AdionFA.Infrastructure.Directories.Contracts;
using AdionFA.Infrastructure.Managements;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace AdionFA.Infrastructure.Directories.Services
{
    public class ProjectDirectoryService : IProjectDirectoryService
    {
        private static bool HasWritePermissionOnPath(string filePath)
        {
            try
            {
                var writeAllow = false;
                var writeDeny = false;

                var accessControlList = new DirectoryInfo(filePath).GetAccessControl();
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

        private static bool ExistDefaultWorkspace()
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
                    var directoryInfo = new DirectoryInfo(ProjectDirectoryManager.ProjectsDirectoryBase());
                    if (directoryInfo.Exists)
                    {
                        // Extractor

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectName));

                        // Node builder

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderNodesUP.GetDescription(), projectName));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderNodesDOWN.GetDescription(), projectName));

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderExtractorWithoutSchedule.GetDescription(), projectName));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Europe.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.America.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.NodeBuilderExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Asia.GetMetadata().Name));

                        // Assembly builder

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesUP.GetDescription(), projectName));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderNodesDOWN.GetDescription(), projectName));

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(), projectName, "UP"));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Europe.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.America.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Asia.GetMetadata().Name));

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorWithoutSchedule.GetDescription(), projectName, "DOWN"));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Europe.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.America.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssemblyBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Asia.GetMetadata().Name));

                        // Crossing builder

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderNodesUP.GetDescription(), projectName));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderNodesDOWN.GetDescription(), projectName));

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(), projectName, "UP"));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Europe.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.America.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Asia.GetMetadata().Name));

                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorWithoutSchedule.GetDescription(), projectName, "DOWN"));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Europe.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.America.GetMetadata().Name));
                        directoryInfo.CreateSubdirectory(string.Format(ProjectDirectoryEnum.CrossingBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Asia.GetMetadata().Name));
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

        public bool CopyCSVFileTo(FileInfo fileInfo, string targetDir)
        {
            try
            {
                if (File.Exists(fileInfo.FullName))
                {
                    File.Copy(fileInfo.FullName, Path.Combine(targetDir, fileInfo.Name), overwrite: true);
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

        private bool CopyCSVFiles(string sourceDir, string targetDir)
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

        public FileInfo[] GetFilesInPath(string filePath, string fileExtension)
        {
            var directory = new DirectoryInfo(filePath);
            var files = directory.GetFiles(fileExtension);

            return files;
        }

        public bool CreateBackup(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    DirectoryInfo di = new(filePath);
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

        public bool DeleteFile(string filePath)
        {
            try
            {
                // Check if file exists with its full path

                if (File.Exists(filePath))
                {
                    // If file found, delete it

                    File.Delete(filePath);

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

        public bool DeleteAllFiles(string sourceDir, string fileExtension, bool doBackup)
        {
            var backupDir = @$"{sourceDir}\Backup_" + DateTime.UtcNow.Ticks;

            try
            {
                var filesToDelete = Directory.GetFiles(sourceDir, fileExtension)
                    .Where(d => !d.Contains("Backup_"))
                    .ToArray();

                if (doBackup)
                {
                    // Copy text files

                    foreach (var file in filesToDelete)
                    {
                        // Get only the file name

                        var fileName = file[(sourceDir.Length + 1)..];

                        try
                        {
                            if (!Directory.Exists(backupDir))
                            {
                                Directory.CreateDirectory(backupDir);
                            }

                            // Will not overwrite if the destination file already exists

                            var targetPath = Path.Combine(backupDir, fileName);

                            File.Copy(Path.Combine(sourceDir, fileName), targetPath);
                        }

                        // Catch exception if the file was already copied

                        catch (IOException copyError)
                        {
                            Trace.TraceError(copyError.Message);
                            throw;
                        }
                    }
                }

                // Delete source files that were copied

                foreach (var file in filesToDelete)
                {
                    File.Delete(file);
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