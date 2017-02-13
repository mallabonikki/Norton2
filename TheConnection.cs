using System;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace Norton2
{
    internal class TheConnection
    {
        private OleDbConnection connection;
        private OleDbCommand command;

        public TheConnection()
        {
            string strPcName = System.Environment.MachineName;
            string strPath = @"\\PC-99\e$\TokenData\" + strPcName + @"\TokenTimer.accdb";

            if (!File.Exists(strPath)) { ErrorLogToText(" Database FILE does not exist or have not enough permission! ", "Database file error"); }
            string strConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + strPath;

            connection = new OleDbConnection(strConString);
        }

        public string SelectLastRowOutDate()
        {
            string s= "";
            try
            {
                connection.Close();
                command = connection.CreateCommand();
                command.CommandText = "SelectLastRow_fOutDate";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                s = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception exp)
            {
                s = "DATABASENOTCONNECTED";
                ErrorLogToText(" SelectLastRowOutDate ", exp.ToString());
            }
            finally
            {
                connection.Close();
            }
            return s;
        }

        private static void ErrorLogToText(string eventname, [Optional] string exp)
        {
            bool IsServerPath = true;
            for (int i = 1; i <= 2; i++)
            {
                string path = (IsServerPath == true) ? @"\\PC-99\e$\TokenData\" : @"C:\\";
                if (!Directory.Exists(path)) return;

                using (StreamWriter writer = new StreamWriter(path + @"\NortonErrors-" + System.Environment.MachineName + ".txt", true))
                {
                    writer.WriteLine(DateTime.Now + eventname + exp);
                }
                IsServerPath = false;
            }
        }

    }
}
