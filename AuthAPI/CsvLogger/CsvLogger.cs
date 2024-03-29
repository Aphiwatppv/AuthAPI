namespace CsvLogger
{
    public static class CsvLogger
    {
        private static readonly string logFilePath = "Logs/log.csv";
        private static readonly object fileLock = new object();

        static CsvLogger()
        {
            // Ensure the directory exists
            var logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Create log file header if the file does not exist
            if (!File.Exists(logFilePath))
            {
                WriteLog("Timestamp,Level,Message");
            }
        }

        public static void LogInformation(string message)
        {
            Log("Information", message);
        }

        public static void LogError(string message)
        {
            Log("Error", message);
        }

        private static void Log(string level, string message)
        {
            string logEntry = $"{DateTime.UtcNow:o},{level},\"{message.Replace("\"", "\"\"")}\"";
            WriteLog(logEntry);
        }

        private static void WriteLog(string logEntry)
        {
            lock (fileLock)
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
        }
    }
}

