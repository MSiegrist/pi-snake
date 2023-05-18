using System.Text;

namespace App
{
    internal static class Logger
    {
        private static FileStream? logFile;

        public static void Initialize()
        {
            logFile = File.Open("snake.log", FileMode.Append, FileAccess.Write, FileShare.Read);
            if (logFile.Position == 0)
            {
                AddLogfileHeader();
            }
        }

        public static void Log(string text)
        {
            string dateString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            string suffix = $": {dateString}";
            lock (logFile)
            {
                logFile.Write(Encoding.ASCII.GetBytes($"{text}{suffix}{Environment.NewLine}"));
            }
        }

        public static void WriteToFile()
        {
            lock (logFile)
            {
                logFile.Flush();
            }
        }

        private static void AddLogfileHeader()
        {
            logFile.Write(Encoding.ASCII.GetBytes($"// Logs from Snake Game - Team 10{Environment.NewLine}"));
        }
    }
}
