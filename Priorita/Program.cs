//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------

using Priorita.projectmanagement;
using Priorita.users;
using Priorita.utilities;

namespace Priorita
{
    /// <summary>
    /// TODO: Make the password more robust
    /// </summary>

    internal class Program
    {
        /// <summary>
        /// Default Values for User and Project Creation
        /// </summary>
        private static readonly string defaultUsername = "root";
        private static readonly string defaultPassword = "sml12345";
        private static readonly string defaultProjectName = "New Project";
        private static readonly string defaultTaskTitle = "New Task";
        private static readonly string defaultTaskDescription = "No Description";
        private static readonly List<User> users = Toolbox.GetUserList();
        private static int currentUserId = 0;

        /// <summary>
        /// Serves as the entry point for the Priorita Project Management Tool application.
        /// </summary>
        /// <remarks>This method initializes the application, displays the main menu, and processes user
        /// input in a loop until the user chooses to exit. It provides access to core features such as creating tasks,
        /// projects, clusters, and users, as well as managing associations between them. The method continues running
        /// until the user confirms the exit action.</remarks>
        /// <param name="args">An array of command-line arguments supplied to the application. This parameter is not used.</param>
        public static void Main(string[] args)
        {
            try { 
            Logger.Log(LogCategory.SYSTEM, "Priorita Project Management Tool started.");
            Printer.PrintHeader("Priorita Project Management Tool");
            Printer.PrintInformation("Create your default user.");
            _ = CreateUser();

            while (true)
            {
                User user = users[currentUserId];
                Printer.ClearConsole();
                Printer.PrintMenu("Main");
                int Choice = GetUserIndex();
                
                switch (Choice)
                {
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        CreateProject();
                        break;
                    case 3:
                        CreateCluster();
                        break;
                    case 4:
                        CreateUser();
                        break;
                    case 5:
                        AddTaskToProject();
                        break;
                    case 6:
                        AddProjectToCluster();
                        break;
                    case 7:
                        ChangeUser();
                        break;
                    case 8:
                        TaskBoardRenderer taskBoard = new(user);
                        taskBoard.PrintBoard();
                        break;
                        case 0:
                        Printer.PrintQuestion("Are you sure you want to exit? (Y/N)");
                        string exitChoice = Console.ReadLine() ?? "N";
                        if (exitChoice.ToUpper() == "Y")
                        {
                            Logger.Log(LogCategory.SYSTEM, "Application shutting down normally.");
                            Printer.ClearConsole();
                            Printer.PrintInformation("Exiting Priorita. Goodbye!");
                            return;
                        }
                        break;
                    default:
                        Printer.PrintGeneralError("Invalid choice. Please try again.");
                        break;
                }
                }
            } catch(Exception e) { 
                Printer.PrintGeneralError($"An unexpected error occurred: {e.Message}");

            }

        }
        /// <summary>
        /// Reads a line from the console and attempts to parse it as an integer representing a user index.
        /// </summary>
        /// <remarks>If the user enters a non-integer value or leaves the input blank, the method returns
        /// 0.</remarks>
        /// <returns>The parsed integer value if the input is a valid integer; otherwise, 0.</returns>
        private static int GetUserIndex() => int.TryParse(Console.ReadLine(), out int index) ? index : 0;

        /// <summary>
        /// Adds a selected task to a selected project for the current user.
        /// </summary>
        /// <remarks>Prompts the user to choose a task and a project from their respective lists, removes
        /// the task from the user's standalone tasks, and associates it with the chosen project. Displays a
        /// confirmation message upon successful completion.</remarks>
        private static void AddTaskToProject()
        {
            Printer.ClearConsole();
            Printer.PrintHeader("Add Task to Project");

            Printer.PrintQuestion("Enter Task ID to add to Project:");
            users[currentUserId].ListProjectTasks();
            int taskId = GetUserIndex();

            Printer.PrintQuestion("Enter Project ID to add Task to:");
            users[currentUserId].ListProjects();
            int projectId = GetUserIndex();

            ProjectTask task = users[currentUserId].ProjectTasks[taskId];
            Project project = users[currentUserId].Projects[projectId];

            users[currentUserId].ProjectTasks.Remove(task);
            project.AddTask(task);

            Printer.PrintInformation($"Task '{task.Title}' added to Project '{project.Name}' successfully.");
        }

        /// <summary>
        /// Adds a selected project to a selected cluster for the current user.
        /// </summary>
        /// <remarks>Prompts the user to choose from available projects and clusters, then moves the
        /// selected project into the chosen cluster. The project is removed from the user's standalone project list and
        /// added to the specified cluster.</remarks>
        private static void AddProjectToCluster()
        {
            Printer.ClearConsole();
            Printer.PrintHeader("Add Project to Cluster");
            Printer.PrintQuestion("Available Projects:");
            users[currentUserId].ListProjects();
            int projectId = GetUserIndex();

            Printer.PrintQuestion("Available Clusters:");
            users[currentUserId].ListClusters();
            int clusterId = GetUserIndex();

            Project project = users[currentUserId].Projects[projectId];
            Cluster cluster = users[currentUserId].Clusters[clusterId];
            users[currentUserId].Projects.Remove(project);
            users[currentUserId].Clusters[clusterId].AddProject(project);

            Printer.PrintInformation($"Project '{project.Name}' added to Cluster '{cluster.Name}' successfully.");
        }

        /// <summary>
        /// Guides the user through the process of creating a new project task by prompting for task details and adding
        /// the task to the current user's project task list.
        /// </summary>
        /// <remarks>This method interacts with the user via the console to collect the task's title,
        /// description, strategic value, and deadline. The new task is added to the current user's collection of
        /// project tasks. The method provides feedback upon successful creation.</remarks>
        private static void CreateTask()
        {
            Printer.ClearConsole();
            Printer.PrintHeader("Create New Task");

            Printer.PrintQuestion("What would you like to name your Task?");
            string taskTitle = GetTaskTitle();

            Printer.PrintQuestion("Provide a brief description of the Task:");
            string taskDescription = GetTaskDescription();

            Printer.PrintQuestion("Set the Strategic Value (1-10):");
            int strategicValue = GetStrategicValue();

            Printer.PrintQuestion("Set the Deadline (yyyy-MM-dd):");
            DateTime deadline = GetDeadline();

            ProjectTask task = new()
            {
                Title = taskTitle,
                Description = taskDescription,
                StrategicValue = strategicValue,
                Deadline = deadline
            };

            users[currentUserId].ProjectTasks.Add(task);
            Logger.Log(LogCategory.TASK, $"User '{users[currentUserId].Username}' created task '{taskTitle}'.");
            Printer.PrintInformation($"Task '{taskTitle}' created successfully.");

        }

        /// <summary>
        /// Reads a line of input from the console and returns it as the task title.
        /// </summary>
        /// <returns>The task title entered by the user, or the default task title if the input is empty or consists only of
        /// white-space characters.</returns>
        private static string GetTaskTitle()
        {
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultTaskTitle : input;
        }

        /// <summary>
        /// Reads a task description from the console input, or returns a default value if the input is empty or
        /// consists only of white space.
        /// </summary>
        /// <returns>The task description entered by the user, or the default task description if no input is provided.</returns>
        private static string GetTaskDescription()
        {
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultTaskDescription : input;
        }

        /// <summary>
        /// Reads a user-provided integer from the console and returns it as the strategic value, defaulting to 5 if the
        /// input is invalid or out of range.
        /// </summary>
        /// <remarks>The method expects the user to enter an integer between 1 and 10, inclusive. If the
        /// input is not a valid integer within this range, an error message is displayed and the method returns
        /// 5.</remarks>
        /// <returns>An integer representing the strategic value entered by the user, or 5 if the input is invalid or outside the
        /// allowed range.</returns>
        private static int GetStrategicValue()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value >= 1 && value <= 10)
            {
                return value;
            }
            else
            {
                Printer.PrintGeneralError("Invalid input. Defaulting Strategic Value to 5.");
                return 5;
            }
        }

        /// <summary>
        /// Reads a date from the console input and returns it as the deadline.
        /// </summary>
        /// <remarks>If the input cannot be parsed as a valid date and time, the method displays an error
        /// message and returns a deadline set to one week from the current date and time.</remarks>
        /// <returns>A <see cref="DateTime"/> value representing the parsed deadline if the input is valid; otherwise, the
        /// current date and time plus seven days.</returns>
        private static DateTime GetDeadline()
        {
            string input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime deadline))
            {
                return deadline;
            }
            else
            {
                Printer.PrintGeneralError("Invalid date format. Defaulting Deadline to one week from today.");
                return DateTime.Now.AddDays(7);
            }
        }

        /// <summary>
        /// Creates a new cluster by prompting the user for a name and description, and adds it to the current user's
        /// collection of clusters.
        /// </summary>
        /// <remarks>This method interacts with the user via the console to gather information about the
        /// new cluster. The created cluster is associated with the currently active user.</remarks>
        private static void CreateCluster()
        {
            Printer.ClearConsole();
            Printer.PrintHeader("Create New Cluster");

            Printer.PrintQuestion("What is the name of the Cluster?");
            string clusterName = GetClusterName();

            Printer.PrintQuestion("Provide a brief description of the Cluster:");
            string clusterDescription = GetClusterDescription();

            Cluster cluster = new() { Name = clusterName };
            users[currentUserId].AddCluster(cluster);
            Printer.PrintInformation($"Cluster '{clusterName}' created successfully.");
        }

        /// <summary>
        /// Reads the cluster name from the console input, returning a default value if the input is empty or
        /// whitespace.
        /// </summary>
        /// <returns>The cluster name entered by the user, or "New Cluster" if no valid input is provided.</returns>
        private static string GetClusterName()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Printer.PrintGeneralError("Cluster name cannot be empty, defaulting to <New Cluster>");
                return "New Cluster";
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Reads a cluster description from the console input, returning a default value if the input is empty or
        /// whitespace.
        /// </summary>
        /// <returns>The cluster description entered by the user, or "No Description" if no input is provided.</returns>
        private static string GetClusterDescription()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Printer.PrintGeneralError("Cluster description cannot be empty, defaulting to <No Description>");
                return "No Description";
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Creates a new project and adds it to the current user's project list.
        /// </summary>
        /// <remarks>The method prompts the user to enter a project name, creates a new project with the
        /// specified name, and associates it with the currently active user. A confirmation message is displayed upon
        /// successful creation.</remarks>
        private static void CreateProject()
        {
            Printer.ClearConsole();
            string projectName = GetProjectName();
            string projectDescription = GetProjectDescription();
            Project project = new() { Name = projectName, Description = projectDescription };
            users[currentUserId].AddProject(project);
            Printer.PrintInformation($"Project '{projectName}' created successfully.");
        }

        /// <summary>
        /// Prompts the user to enter the name of the project and returns the provided value.
        /// </summary>
        /// <returns>The project name entered by the user, or a default project name if the input is empty or consists only of
        /// whitespace.</returns>
        private static string GetProjectName()
        {
            Printer.PrintQuestion("What is the name of the Project?");
            string userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Printer.PrintGeneralError("Project name cannot be empty, defaulting to <New Project>");
                return defaultProjectName;
            }
            else
            {
                return userInput;
            }
        }

        /// <summary>
        /// Prompts the user to enter a brief description of the project and returns the input.
        /// </summary>
        /// <returns>A string containing the project description entered by the user.  Returns "No Description" if the user
        /// provides no input or only whitespace.</returns>
        private static string GetProjectDescription()
        {
            Printer.PrintQuestion("Provide a brief description of the Project:");
            string userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Printer.PrintGeneralError("Project description cannot be empty, defaulting to <No Description>");
                return "No Description";
            }
            else
            {
                return userInput;
            }
        }

        /// <summary>
        /// Creates a new <see cref="User"/> instance using input collected from the user.
        /// </summary>
        /// <remarks>The method prompts for a username and password, creates a new <see cref="User"/>, and
        /// adds it to the internal user collection.</remarks>
        /// <returns>A <see cref="User"/> object initialized with the provided username and password.</returns>
        private static User CreateUser()
        {
            Printer.ClearConsole();
            string name = GetUsername();
            string password = GetUserPassword();
            User user = new(name, password);
            users.Add(user);
            Printer.PrintSuccess($"User '{name}' created successfully.");

            return user;
        }

        /// <summary>
        /// Prompts the user to enter a username and returns the entered value, or a default username if the input is
        /// empty or whitespace.
        /// </summary>
        /// <returns>The username entered by the user, or a default username if the input is empty or consists only of
        /// whitespace.</returns>
        private static string GetUsername()
        {
            Printer.PrintQuestion("What is the name of the User?");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Printer.PrintGeneralError("User name cannot be empty, defaulting to <root>");
                return defaultUsername;
            }
            else
            {
                return userInput;
            }
        }

        /// <summary>
        /// Prompts the user to enter a password and returns the entered value, or a default password if the input is invalid.
        /// </summary>
        /// <returns>A string containing the password entered by the user, or the default password if the input does not meet the
        /// requirements.</returns>
        private static string GetUserPassword()
        {
            Printer.PrintQuestion("Set a password for the User:");
            string userInput = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(userInput))
            {
                Printer.PrintGeneralError("Password does not meet the requirements, defaulting to <sml12345>");
                return defaultPassword;
            }
            else
            {
                return userInput;
            }
        }

        /// <summary>
        /// Prompts the user to select an existing user by ID and updates the current user accordingly.
        /// </summary>
        /// <remarks>This method displays a list of available users and waits for the user to enter a
        /// valid user ID.  If no users are available, an error message is displayed and the operation is aborted.  The
        /// current user is updated only when a valid user ID is provided.</remarks>
        private static void ChangeUser()
        {
            if (users.Count == 0)
            {
                Printer.PrintGeneralError("No users available. Please create a user first.");
                return;
            }
            Printer.PrintQuestion("Select a user by ID:");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i}) {users[i].Username}");
            }
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selectedUserId) && selectedUserId >= 0 && selectedUserId < users.Count)
                {
                    currentUserId = selectedUserId;
                    Logger.Log(LogCategory.SYSTEM, $"Active user switched to: {users[currentUserId].Username}");
                    Printer.PrintSuccess($"Switched to user: {users[currentUserId].Username}");
                    break;
                }
                else
                {
                    Printer.PrintGeneralError("Invalid user ID. Please try again.");
                }
            }

        }
    }
}
