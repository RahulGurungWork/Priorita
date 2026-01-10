namespace Priorita.Utility 
{
    public static class Logger
    {
        private static readonly string LogFilePath = "priorita_system.log";
        public static void Log(string category, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [{category.ToUpper()}] {message}";

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(logEntry);
            Console.ResetColor();

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