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
using System.IO;

namespace Norton2
{
    public partial class Service1 : ServiceBase
    {
        //fields
        //FileStream fs;
        //StreamWriter m_streamWriter;
        private Timer ShutdownTimer = new Timer();
        public static string originalFile = "";
        public static string modifiedFile = "";
        private Timer SecondTimer = new Timer();


        public Service1()
        {
            InitializeComponent();

            //fs = new FileStream(@"C:\mcWindowsService.txt", FileMode.OpenOrCreate, FileAccess.Write);

            //m_streamWriter = new StreamWriter(fs);
            //m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
        }

#if (DEBUG)
        public void OnDebug()
        {
            OnStart(null);
        }
#endif

        private System.Timers.Timer timer;
        protected override void OnStart(string[] args)
        {
            
            this.timer = new System.Timers.Timer(100000D);
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //FileInfo fileInfo = new FileInfo("\\\\PC-99\\e$\\TokenData\\" + "PC-01" + "\\TokenTimer.accdb");
            //DateTime lastWriteTime = fileInfo.LastWriteTime;
            //originalFile = lastWriteTime.ToString();

            //string sourceFile = Path.Combine("\\\\PC-99\\e$\\TokenData\\" + "PC-01", "TokenTimer.accdb");
            //string destFile = Path.Combine("C:\\Norton2", "TokenTimer.accdb");
            //File.Copy(sourceFile, destFile, true);

            Norton2.ServiceWork.Main2();
        }

        protected override void OnStop()
        {
            this.timer.Stop();
        }
    }   
}

