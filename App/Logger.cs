using System.Text;

namespace App
{
    internal static class Logger
    {
        private static FileStream? logFile;
        private static string? filePath;
        private static readonly object lockObject = new object(); // Add a dedicated lock object
        private static string lastLog;

        public static void Initialize()
        {
            filePath = SimpleHttpServer.SimpleHttpServer.fileName;
            logFile = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            if (logFile.Position == 0)
            {
                AddLogfileHeader();
            }
        }

        public static void Log(string text)
        {
            string dateString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            string suffix = $": {dateString}";

            if (text == lastLog)
            {
                return; //Skip logging if it's the same text 
            }

            //else input the text into variable "lastlog"
            lastLog = text;

            lock (lockObject)
            {
                logFile.Write(Encoding.ASCII.GetBytes($"{text}{suffix}{Environment.NewLine}"));
            }
        }

        public static void WriteToFile()
        {
            lock (lockObject)
            {
                logFile.Flush();
            }
        }

        private static void AddLogfileHeader()
        {
            logFile.Write(Encoding.ASCII.GetBytes($"// Logs from Snake Game - Team 10{Environment.NewLine}"));
        }

        public static void ClearLastLog()
        {
            lastLog = string.Empty;
        }
    }
}
