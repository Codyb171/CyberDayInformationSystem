using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CyberDayInformationSystem.App_Code
{
    public class WriteDocument
    {
        public string WritePermission(string file, string[] info)
        {
            
            string fileName =file + ".txt" ;
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (string s in info)
                {
                    sw.WriteLine(s);
                }

            }
            
            return fileName;
        }

    }
}