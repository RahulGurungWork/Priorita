//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------


namespace Priorita.utilities
{
    /// <summary>
    /// A class for logging messages with different categories.
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFilePath = "priorita_system.log";
        /// <summary>
        /// Logs a message with a specified category to a log file.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static void Log(LogCategory category, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [{category}] {message}";

            try
            {
                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOGGER ERROR] Could not write to file: {ex.Message}");
            }
        }
    }
}