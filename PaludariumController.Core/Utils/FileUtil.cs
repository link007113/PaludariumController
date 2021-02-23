using System;
using System.Collections.Generic;
using System.Text;

namespace PaludariumController.Core.Utils
{
   public class FileUtil
    {
        public static string GetFilePath(String file)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string result = System.IO.Path.Combine(strWorkPath, file);
            return result;
        }
        public static string GetWorkingDir()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string result = System.IO.Path.GetDirectoryName(strExeFilePath);
            return result;
        }
    }
}
