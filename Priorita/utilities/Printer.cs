//-----------------------------------------------------------------------
// <copyright file="Printer.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------



using Priorita.projectmanagement;

namespace Priorita.utilities
{
    /// <summary>
    /// A class representing various printing utilities for the console interface.
    /// </summary>
    internal class Printer
    {
        /// <summary>
        /// Prints a formatted header with the given title.
        /// </summary>
        /// <param name="title"></param>
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

        /// <summary>
        /// Prints a question to the console.
        /// </summary>
        /// <param name="question"></param>
        public static void PrintQuestion(string question)
        {
            Console.WriteLine(question);
        }
        /// <summary>
        /// Prints help information to the console.
        /// </summary>
        /// <param name="help"></param>
        public static void PrintHelp(string help) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(help);
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Prints a general error message to the console and logs it.
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void PrintGeneralError(string errorMessage)
        {
            Logger.Log(LogCategory.ERROR, errorMessage);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {errorMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Clears the console screen.
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Prints informational message to the console and waits for user to press ENTER.
        /// </summary>
        /// <param name="information"></param>
        public static void PrintInformation(string information)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(information);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press ENTER to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

        }
        /// <summary>
        /// Prints a success message to the console.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS]: {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints a menu based on the specified menu type.
        /// </summary>
        /// <param name="menuType"></param>
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

        /// <summary>
        /// Prints the details of a cluster, including its projects and tasks.
        /// </summary>
        /// <param name="cluster"></param>
        public static void PrintCluster(Cluster cluster)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[CLUSTER] {cluster.Name.ToUpper()}");
            Console.ResetColor();

            if (cluster.Projects.Count == 0)
            {
                Console.WriteLine("    (No projects in this cluster)");
            }
            else
            {
                foreach (Project project in cluster.Projects)
                {
                    PrintProject(project, indentLevel: 1);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the details of a project, including its tasks.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="indentLevel"></param>
        public static void PrintProject(Project project, int indentLevel)
        {
            string indent = new(' ', indentLevel * 4);
            Console.WriteLine($"{indent}└── [PROJ] {project.Name}");

            if (project.Tasks.Count == 0)
            {
                Console.WriteLine($"{indent}    (No tasks)");
                return;
            }

            List<ProjectTask> sortedTasks = Toolbox.SortTasksByPriority(project.Tasks);

            foreach (ProjectTask task in sortedTasks)
            {
                PrintTask(task, indentLevel + 1);
            }
        }

        /// <summary>
        /// Prints the details of a task with indentation based on its level.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="indentLevel"></param>
        public static void PrintTask(ProjectTask task, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 4);
            string priorityLabel = task.GetQuadrantLabel();
            Console.WriteLine($"{indent}> {priorityLabel} {task.Title} (Due: {task.TimeCriticality}d)");
        }

    }
}