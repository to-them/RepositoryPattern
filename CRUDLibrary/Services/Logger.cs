using CRUDLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Services
{
    public class Logger : Base, ILogger
    {
        private string _logFolder { get; set; } //Base class will locate it in the config file
        public Logger()
        {
            _logFolder = this._logFolderPath;
        }

        public void Log(string message, string log_type)
        {
            WriteLogToTextFile(message, log_type);
        }

        private void WriteLogToTextFile(string log_message, string log_type)
        {
            StreamWriter sw = new StreamWriter(_getTextFile, true);
            switch (log_type)
            {
                case "Error":
                    sw.WriteLine($"{DateTime.Now} | Exception caught: {log_message}");
                    break;

                case "Info":
                    sw.WriteLine($"{DateTime.Now} | Info: {log_message}");
                    break;

                case "Warning":
                    sw.WriteLine($"{DateTime.Now} | Warning: {log_message}");
                    break;

                default:
                    sw.WriteLine($"{DateTime.Now} | Details: {log_message}");
                    break;
            }

            sw.Flush();
            sw.Close();
        }

        //Make text file by month
        private string _getTextFile
        {
            get
            {
                string s = string.Empty;
                DirectoryInfo dir = new DirectoryInfo(_logFolder);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                int y = DateTime.Now.Year;
                int m = DateTime.Now.Month;
                s = $"{_logFolder}Log_{y}-{m}.txt";
                return s;
            }
        }
    }
}
