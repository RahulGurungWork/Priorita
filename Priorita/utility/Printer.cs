namespace Priorita.utility
{
    internal class Printer
    {
        public static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            int length = title.Length + 10;

            for (int i = 0; i < length; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.Write(" ");
            }

            Console.Write(title);

            Console.WriteLine();

            for (int i = 0; i < length; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintQuestion(string question)
        {
            Console.WriteLine(question);
        }

        public static void PrintHelp(string help) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(help);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintGeneralError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {errorMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void PrintInformation(string information)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(information);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

        }

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS]: {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintMenu(string menuType)
        {
            if (!string.IsNullOrEmpty(menuType))
            {
                switch (menuType.ToLower())
                {
                    case "main":
                        PrintHeader("MAIN MENU");
                        Console.WriteLine(Menu.GetMainMenu());
                        break;
                }
            }
            else
            {
                PrintGeneralError("Menu type cannot be null or empty.");

            }
        }
    }
}