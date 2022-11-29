using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Live_File_Organizer.Model
{
    public class GetJsonData
    {
        public static SettingsJson GetJson()
        {
            SettingsJson settings = new()
            {
                SourcePath = GetDownloadFolderPath(),
                File_Extension_And_Custom_DestPath =
                {
                    new List<string>() { ".exe", "SetTargetPath" },
                    new List<string>() { ".png", "SetTargetPath" }
                },
                Delay_In_Minutes = 5,
                Sort_Folders_Into_Specific_Extension_Folders = true,
            };

            // Json Serialize
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);

            // Read Settings or Create if not exists
            jsonString = JsonStreamReader(jsonString);

            // Json Deserialize
            return JsonSerializer.Deserialize<SettingsJson>(jsonString);
        }
        
        private static string JsonStreamReader(string jsonString) // Read the Settings File
        {
            JsonFileExistCheck(jsonString);

            StreamReader streamReader = new(@"Settings\Settings.json");

            jsonString = streamReader.ReadToEnd();

            streamReader.Close();

            return jsonString;
        }

        private static void JsonStreamWriter(string jsonString) // Create .json File
        {
            JsonDirectoryExistCheck();

            StreamWriter streamWriter = new(@"Settings\Settings.json");

            streamWriter.WriteLine(jsonString);

            streamWriter.Close();
        }

        private static void JsonFileExistCheck(string jsonString)
        {
            if (!File.Exists(@"Settings\Settings.json"))
                JsonStreamWriter(jsonString);
        }

        private static void JsonDirectoryExistCheck()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Settings\"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Settings");
        }

        private static string GetDownloadFolderPath()
        {
            try
            {
                return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
            }
            catch (Exception)
            {
                return "Set Download Path (or Source Path)";
            }
        }
    }
}
