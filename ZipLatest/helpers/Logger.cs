using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipLatest.helpers
{
    public class Logger
    {
        private static string _filePath = string.Empty;

        public static void WriteEntry(string contents, bool showInConsole = false)
        {
            if (showInConsole)
            {
                if (!string.IsNullOrEmpty(contents))
                {
                    Console.WriteLine(contents);
                } 
            }

            if (!_UseLogging)
            {
                return;
            }

            bool createHeader = false;

            if (string.IsNullOrEmpty(_filePath))
            {

                string fileName = string.Format("ZipLatest {0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
                string dirPath = AppDomain.CurrentDomain.BaseDirectory;

                _filePath = System.IO.Path.Combine(dirPath, fileName);

                // CREATE HEADER IF NEW FILE
                if (!System.IO.File.Exists(_filePath))
                {
                    createHeader = true;
                }
            }

            using (var writer = new System.IO.StreamWriter(_filePath, true))
            {
                if (createHeader)
                {
                    writer.WriteLine("ZipLatest - {0}", DateTime.Now.ToString("dd MMMMM yyyy"));
                    writer.WriteLine("================================================");
                    writer.WriteLine("");
                }

                writer.Write("{0} - ", DateTime.Now.ToString("HH:mm:ss"));
                writer.Write(contents);

                writer.WriteLine("");
            }

        }

        private static bool _UseLogging = false;

        public static bool UseLogging
        {
            get { return _UseLogging; }
            set { _UseLogging = value; }
        }

    }
}
