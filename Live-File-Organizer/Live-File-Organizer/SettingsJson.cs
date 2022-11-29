using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live_File_Organizer
{
    public class SettingsJson
    {
        public string SourcePath { get; set; } = "";
        public List<List<string>> File_Extension_And_Custom_DestPath { get; set; }
        public int Delay_In_Minutes { get; set; }
        public bool Sort_Folders_Into_Specific_Extension_Folders { get; set; }

        public SettingsJson()
        {
            File_Extension_And_Custom_DestPath = new List<List<string>>();
        }
    }
}
