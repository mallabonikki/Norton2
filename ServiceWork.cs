using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norton2
{
    class ServiceWork
    {
        internal static void Main2()
        {


            //FileInfo fileInfo = new FileInfo("\\\\PC-99\\e$\\TokenData\\" + System.Environment.MachineName + "\\TokenTimer.accdb");

            //string sourceFile = Path.Combine("\\\\PC-99\\e$\\TokenData\\" + "PC-01", "TokenTimer.accdb");
            //string destFile = Path.Combine("C:\\Norton2", "TokenTimer.accdb");
            //File.Copy(sourceFile, destFile, true);

            FileInfo fileInfo = new FileInfo("C:\\Norton2\\TokenTimer.accdb");

            DateTime lastWriteTime = fileInfo.LastWriteTime;
            Service1.modifiedFile = lastWriteTime.ToString();

            if (Service1.originalFile == Service1.modifiedFile)
            {
                //Console.WriteLine("Shutdown this PC");
                //Console.WriteLine(Service1.originalFile + " ====" + Service1.modifiedFile);
                //ErrorLogToText("Shutdown ", DateTime.Now.ToString());
                //System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Process.Start("shutdown", "/s /t 0 /f");
            }
            else
            {
                Service1.originalFile = Service1.modifiedFile;
            }
        }
    }
}
