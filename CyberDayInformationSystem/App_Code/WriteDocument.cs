using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CyberDayInformationSystem.App_Code
{
    public class WriteDocument
    {
        public string SaveFile(string file, string info)
        {
            string path = "~/Student_Permissions/" + file + ".txt";
            
            return path;
        }
    }
}