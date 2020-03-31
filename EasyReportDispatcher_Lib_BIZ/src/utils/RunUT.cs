using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_Lib_BIZ.src.utils
{
    public class RunUT
    {

        /// <summary>
        /// Copia un file tramite xcopy con eventuali credenziali alternative
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationDirPath"></param>
        /// <param name="domain"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public static void XCopyTo(string sourceFilePath, string destinationDirPath, string domain, string user, string pass)
        {
            var pwd = new System.Security.SecureString();

            foreach (char c in pass)
            {
                pwd.AppendChar(c);
            }

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UserName = user;
            startInfo.Password = pwd;
            startInfo.Domain = domain;
            //startInfo.Verb = "runas";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;


            startInfo.FileName = "xcopy";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.Arguments = "\"" + sourceFilePath + "\"" + " " + "\"" + destinationDirPath + "\"" + @" /e /y /I";

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

    }
}
