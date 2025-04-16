using System;
using System.IO;

namespace ECommerce.Utilities
{
    public static class Logger
    {
        private static readonly string logFilePath = "log.txt";

        public static void LogInfo(string message)
        {
            File.AppendAllText(logFilePath, $"INFO [{DateTime.Now}]: {message}\n");
        }

        public static void LogError(string message)
        {
            File.AppendAllText(logFilePath, $"ERROR [{DateTime.Now}]: {message}\n");
        }
    }
}