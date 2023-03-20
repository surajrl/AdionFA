using Adion.FA.Infrastructure.Common.Base;
using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Logger.Helpers;
using Adion.FA.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Adion.FA.Infrastructure.Common.Directories.Services
{
    public class ProjectDirectoryService : InfrastructureServiceBase, IProjectDirectoryService
    {
        #region Ctor
        public ProjectDirectoryService() : base()
        {
        }
        #endregion

        public bool HasWritePermissionOnPath(string path)
        {
            try
            {
                var writeAllow = false;
                var writeDeny = false;
                var accessControlList = new DirectoryInfo(path).GetAccessControl();
                if (accessControlList == null)
                    return false;
                var accessRules = accessControlList.GetAccessRules(true, true,
                                            typeof(System.Security.Principal.SecurityIdentifier));
                if (accessRules == null)
                    return false;

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                        continue;

                    if (rule.AccessControlType == AccessControlType.Allow)
                        writeAllow = true;
                    else if (rule.AccessControlType == AccessControlType.Deny)
                        writeDeny = true;
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
                string workspacePath = ProjectDirectoryManager.DefaultDirectory();

                DirectoryInfo di = new DirectoryInfo(workspacePath);
                if (!Directory.Exists(workspacePath) /*&& HasWritePermissionOnPath(workspacePath)*/)
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
                    DirectoryInfo di = new DirectoryInfo(ProjectDirectoryManager.ProjectsDirectoryBase());
                    if (di.Exists)
                    {
                        #region Extractor
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectName, MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectName, MarketRegionEnum.Asia.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorWithoutSchedule.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectName));
                        #endregion

                        #region StrategyBuilder
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderNodesUP.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.StrategyBuilderNodesDOWN.GetDescription(), projectName));
                        #endregion

                        #region AssembledBuilder
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderNodesUP.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderNodesDOWN.GetDescription(), projectName));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorWithoutSchedule.GetDescription(), projectName, "UP"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "UP", MarketRegionEnum.Asia.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorWithoutSchedule.GetDescription(), projectName, "DOWN"));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Europe.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.America.GetMetadata().Name));
                        di.CreateSubdirectory(string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectName, "DOWN", MarketRegionEnum.Asia.GetMetadata().Name));
                        #endregion
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

        public bool IsValidProjectDiractory(string projectName)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(projectName.ProjectDirectory());
                if (di.Exists)
                {
                    List<DirectoryInfo> extractorDirectories = di.GetDirectories().FirstOrDefault(
                        sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorDirectory().Replace(@"\", string.Empty))?.GetDirectories().ToList() ?? new List<DirectoryInfo>();
                    bool extratorDirectoryValidate = extractorDirectories.Any(sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorEuropeDirectory().Replace(@"\", string.Empty)) &&
                                                     extractorDirectories.Any(sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorAmericaDirectory().Replace(@"\", string.Empty)) &&
                                                     extractorDirectories.Any(sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorAsiaDirectory().Replace(@"\", string.Empty)) &&
                                                     extractorDirectories.Any(sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorWithoutScheduleDirectory().Replace(@"\", string.Empty)) &&
                                                     extractorDirectories.Any(sub => sub.FullName.Replace(@"\", string.Empty) == projectName.ProjectExtractorTemplatesDirectory().Replace(@"\", string.Empty));
                    return extratorDirectoryValidate;
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
                    using (FileStream SourceStream = File.Open(fi.FullName, FileMode.Open))
                    {
                        using (FileStream DestinationStream = File.Create(Path.Combine(targetDir, fi.Name)))
                        {
                            await SourceStream.CopyToAsync(DestinationStream);
                            return true;
                        }
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

        public bool CopyCSVFiles(string sourceDir, string targetDir)
        {
            try
            {
                if (Directory.Exists(sourceDir) && Directory.Exists(targetDir) && HasWritePermissionOnPath(targetDir))
                {
                    IEnumerable<string> txtFiles = Directory.EnumerateFiles(sourceDir, "*.csv");
                    foreach (string currentFile in txtFiles)
                    {
                        string fileName = currentFile.Substring(sourceDir.Length + 1);
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
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles(ext);
            return Files;
        }

        public bool CreateBackup(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    DirectoryInfo bu = di.CreateSubdirectory("Backup_" + DateTime.UtcNow.Ticks);
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
                LogHelper.LogException<ProjectDirectoryService>(ex);
                throw;
            }
        }

        public bool DeleteAllFiles(string sourceDir, string ext = "*.csv", SearchOption option = 0, bool overwrite = false, bool isBackup = true)
        {
            string backupDir = @$"{sourceDir}\Backup_" + DateTime.UtcNow.Ticks;
            
            try
            {
                string[] csvList = Directory.GetFiles(sourceDir, ext, option).Where(d => !d.Contains("Backup_")).ToArray();

                if (isBackup)
                {
                    // Copy text files.
                    foreach (string f in csvList)
                    {

                        // Remove path from the file name.
                        string fName = f.Substring(sourceDir.Length + 1);

                        try
                        {
                            if (!Directory.Exists(backupDir))
                                Directory.CreateDirectory(backupDir);
                            // Will not overwrite if the destination file already exists.
                            string targetPath = Path.Combine(backupDir, option == SearchOption.AllDirectories ? Path.GetFileName(fName) : fName);
                            File.Copy(Path.Combine(sourceDir, fName), targetPath, overwrite);
                        }

                        // Catch exception if the file was already copied.
                        catch (IOException copyError)
                        {
                            LogHelper.LogException<ProjectDirectoryService>(copyError);
                            throw;
                        }
                    }
                }

                // Delete source files that were copied.
                foreach (string f in csvList)
                {
                    File.Delete(f);
                }

                return true;
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                LogHelper.LogException<ProjectDirectoryService>(dirNotFound);
                throw;
            }
        }
    }

    public static class ProjectDirectoryManager
    {
        public static string defaultWorkspace { get; set; }

        public static string DefaultDirectory() => 
            string.Format(@"{0}\{1}", !string.IsNullOrWhiteSpace(defaultWorkspace)
                ? defaultWorkspace
                : Environment.GetFolderPath((Environment.SpecialFolder)ProjectDirectoryEnum.DefaultWorkspace)
            , ProjectDirectoryEnum.DefaultWorkspace.GetDescription());

        public static string ProjectsDirectoryBase() => string.Format(@"{0}\{1}", DefaultDirectory(), ProjectDirectoryEnum.Projects.GetDescription());

        public static string ProjectDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(), projectNameFolder);
        }

        #region Extrator
        public static string ProjectExtractorDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.Extractor.GetDescription(), projectNameFolder));
        }

        public static string ProjectExtractorEuropeDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorAmericaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorAsiaDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorMarket.GetDescription(), projectNameFolder, MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorWithoutScheduleDirectory(this string projectNameFolder, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorWithoutSchedule.GetDescription(), projectNameFolder)) 
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectExtractorTemplatesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.ExtractorTemplate.GetDescription(), projectNameFolder));
        }
        #endregion

        #region Strategy Builder
        public static string ProjectStrategyBuilderDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilder.GetDescription(), projectNameFolder));
        }

        public static string ProjectStrategyBuilderNodesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodes.GetDescription(), projectNameFolder));
        }

        public static string ProjectStrategyBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectStrategyBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.StrategyBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }
        #endregion

        #region Assembled Builder
        public static string ProjectAssembledBuilderDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilder.GetDescription(), projectNameFolder));
        }

        #region Extractor
        public static string ProjectAssembledBuilderExtractorUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderExtractorDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorDOWN.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderExtractorWithoutScheduleDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorWithoutSchedule.GetDescription(), projectNameFolder,
                label.ToLower() == "up" ? "UP" : "DOWN"))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorEuropeDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Europe.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorAmericaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.America.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }

        public static string ProjectAssembledBuilderExtractorAsiaDirectory(this string projectNameFolder, string label, string withFileName = null)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderExtractorMarket.GetDescription(), projectNameFolder
                , label.ToLower() == "up" ? "UP" : "DOWN"
                , MarketRegionEnum.Asia.GetMetadata().Name))
                + (!string.IsNullOrWhiteSpace(withFileName) ? @$"\{withFileName}" : string.Empty);
        }
        #endregion

        #region Nodes
        public static string ProjectAssembledBuilderNodesDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodes.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderNodesUPDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodesUP.GetDescription(), projectNameFolder));
        }

        public static string ProjectAssembledBuilderNodesDOWNDirectory(this string projectNameFolder)
        {
            return string.Format(@"{0}\{1}", ProjectsDirectoryBase(),
                string.Format(ProjectDirectoryEnum.AssembledBuilderNodesDOWN.GetDescription(), projectNameFolder));
        }
        #endregion

        #endregion
    }
}
