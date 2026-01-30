using System;
using System.IO;

namespace ECI.Test.Utilities
{
    public interface ILogger
    {
        void LogError(string source, string operation, Exception ex);
        void LogInfo(string source, string message);
    }

    public class Logger : ILogger
    {
        private static readonly string LogFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "ECI.Test", 
            "logs", 
            $"error_{DateTime.Now:yyyy-MM-dd}.log");

        static Logger()
        {
            var logDir = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        public void LogError(string source, string operation, Exception ex)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR - {source}.{operation}\n" +
                              $"Message: {ex.Message}\n" +
                              $"StackTrace: {ex.StackTrace}\n" +
                              $"----------------------------------------\n";

                Console.WriteLine(logEntry);
                File.AppendAllText(LogFilePath, logEntry);
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Failed to write to log file: {logEx.Message}");
                Console.WriteLine($"Original error - {source}.{operation}: {ex.Message}");
            }
        }

        public void LogInfo(string source, string message)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO - {source}: {message}\n";
                Console.WriteLine(logEntry);
                File.AppendAllText(LogFilePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write info log: {ex.Message}");
            }
        }
    }
}