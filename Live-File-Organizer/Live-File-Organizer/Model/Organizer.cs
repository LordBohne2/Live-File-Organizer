using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live_File_Organizer.Model
{
    internal class Organizer
    {
        public SettingsJson Settings { get; set; }

        public Organizer() 
        {
            Settings = GetJsonData.GetJson();
        }

        public void StartOrganizer()
        {
            // Get Files Path
            string[] filesPath = Directory.GetFiles(Settings.SourcePath);
            bool foundFileWithExtension;

            if (filesPath.Length > 0)
            {
                foreach (string filePath in filesPath)
                {
                    foundFileWithExtension = false;
                    // Move Specific Files to Custum Path
                    if (Settings.File_Extension_And_Custom_DestPath.Count > 0)
                    {
                        foreach (List<string> extensionAndPath in Settings.File_Extension_And_Custom_DestPath)
                        {
                            if (Path.GetExtension(filePath).Contains(extensionAndPath[0]))
                            {
                                MoveFilesToTarget(filePath, extensionAndPath[1], Path.GetFileName(filePath));
                                foundFileWithExtension = true;
                                break;
                            }
                        }
                    }
                    // Move Files to the Extension Folder
                    if (Settings.Sort_Folders_Into_Specific_Extension_Folders && !foundFileWithExtension)
                    {
                        // Get Folder Name for File Extension
                        string destFolderPath = Path.GetExtension(filePath);
                        destFolderPath = destFolderPath.Replace(".", "");

                        string targetExtensionFolder = Settings.SourcePath + "\\" + destFolderPath;

                        MoveFilesToTarget(filePath, targetExtensionFolder, Path.GetFileName(filePath));
                    }
                }
            }
        }

        private static void MoveFilesToTarget(string sourceFile, string targetPath, string fileName)
        {
            FolderExistsCheck(targetPath);

            if (File.Exists(sourceFile) && Directory.Exists(targetPath))
                File.Move(sourceFile, targetPath + "\\" + fileName);
        }

        private static void FolderExistsCheck(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public int GetMillisecondsFromMinutes()
        {
            // 1s = 1000ms
            // 1m = 60s
            return (Settings.Delay_In_Minutes * 60) * 1000;
        }
    }
}
