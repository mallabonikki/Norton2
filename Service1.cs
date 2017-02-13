using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace Norton2
{
    public partial class Service1 : ServiceBase
    {
        //fields
        private Timer tmrShutdown = new Timer();
        private string oldTime = "";
        private string currentTime = "";
        private TheConnection connection;
        //Process[] pname = Process.GetProcessesByName("Token Timer v4");

        public Service1()
        {
            InitializeComponent();
            tmrShutdown.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            tmrShutdown.Interval = 70000;
            tmrShutdown.Enabled = true;
        }

        [Conditional("DEBUG")]
        static void DebugMode()
        {
            if (!Debugger.IsAttached) Debugger.Launch();

            Debugger.Break();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //string pcname = System.Environment.MachineName;
            try
            {   //check Token Timer if it is not running
                //OpenWithStartInfo();
                //System.Threading.Thread.Sleep(5000);

                connection = new TheConnection();
                currentTime = connection.SelectLastRowOutDate();
                ErrorLogToText( " - currentTime = "+ currentTime + " oldTime = " + oldTime);
                if (currentTime == oldTime)
                {
                    ErrorLogToText(" - TimerCheker Activated!", System.Environment.MachineName + " Token Timer is not running!");
                    //System.Diagnostics.Process.Start("shutdown", "/s /t 0 /f");
                    ErrorLogToText("Shutdown!");
                    System.Threading.Thread.Sleep(5000);
                }
                oldTime = currentTime;
                ErrorLogToText(" - AFTER - currentTime = " + currentTime + " oldTime = " + oldTime); ;
            }
            catch (Exception exp)
            {
                ErrorLogToText("OnTimedEvent Error!", exp.ToString());
            }
        }

        private static void ErrorLogToText(string eventname, [System.Runtime.InteropServices.Optional] string exp)
        {
            bool IsServerPath = true;
            for (int i = 1; i <= 2; i++)
            {
                string path = (IsServerPath == true) ? @"\\PC-99\e$\TokenData\" : @"C:\\";
                if (!System.IO.Directory.Exists(path)) return;

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + @"\NortonErrors-" + System.Environment.MachineName + ".txt", true))
                {
                    writer.WriteLine(DateTime.Now + eventname + exp);
                }
                IsServerPath = false;
            }
        }

        //private void OpenWithStartInfo()
        //{
        //    Process myProcess = new Process();
        //    try
        //    {
        //        myProcess.StartInfo.UseShellExecute = true;
        //        // You can start any process, HelloWorld is a do-nothing example.
        //        myProcess.StartInfo.FileName = "C:\\Token\\Token Timer v4.exe";
        //        myProcess.StartInfo.CreateNoWindow = false;
        //        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        //        myProcess.Start();
        //        // This code assumes the process you are starting will terminate itself. 
        //        // Given that is is started without a window so you cannot terminate it 
        //        // on the desktop, it must terminate itself or you can do it programmatically
        //        // from this application using the Kill method.
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        protected override void OnStart(string[] args)
        { 
            //DebugMode();
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerAsync();

            tmrShutdown.Start();
        }

        //private void bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Process p = new Process();
        //    p.StartInfo = new ProcessStartInfo("C:\\Token\\Token Timer v4.exe");
        //    p.Start();
        //    p.WaitForExit();
        //    base.Stop();
        //}

        protected override void OnStop()
        //DebugMode();
        {   //stoping service while token timer is running. computr will shutdown
            //Process[] pname = Process.GetProcessesByName("Token Timer v4");

            //try
            //{
            //    System.Threading.Thread.Sleep(5000);
            //    if (pname[0].HasExited)
            //    {
            //        tmrShutdown.Stop();
            //    }
            //    else
            //    {
            //        TheConnection.ErrorLogToText(" - Illegal Stop Windows Service!");
            //        System.Diagnostics.Process.Start("shutdown", "/s /t 0 /f");
            //    }
            //}
            //catch (Exception exp)
            //{
            //    TheConnection.ErrorLogToText(" - Norton2 Windows Service Error. ", exp.ToString());
            //    System.Diagnostics.Process.Start("shutdown", "/s /t 0 /f");
            //}
            tmrShutdown.Stop();
        }
    }
}

