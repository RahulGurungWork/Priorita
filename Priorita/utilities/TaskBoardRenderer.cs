//-----------------------------------------------------------------------
// <copyright file="TaskBoardRenderer.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------



using Priorita.projectmanagement;
using Priorita.users;

namespace Priorita.utilities
{

    /// <summary>
    /// A class responsible for rendering the Task Board view for a user.
    /// </summary>
    /// <param name="user"></param>
    public class TaskBoardRenderer(User user)
    {
        /// <summary>
        /// Prints the Task Board view to the console.
        /// </summary>
        public void PrintBoard()
{
            Logger.Log(LogCategory.INFO, $"User '{user.Username}' generated the Task Board view.");
            Printer.ClearConsole();
            Printer.PrintHeader("PRIORITA TASK BOARD");

            if (user.Clusters.Count != 0)
            {
                Printer.PrintHeader("CLUSTERS & AREAS");
                foreach (Cluster cluster in user.Clusters)
                {
                    Printer.PrintCluster(cluster);
                }
            }

            if (user.Projects.Count != 0)
            {
                Printer.PrintHeader("UNCATEGORIZED PROJECTS");
                foreach (Project project in user.Projects)
                {
                    Printer.PrintProject(project, indentLevel: 1);
                }
            }
            if (user.ProjectTasks.Count != 0)
            {
                Printer.PrintHeader("UNCATEGORIZED TASKS");
                List<ProjectTask> sortedTasks = Toolbox.SortTasksByPriority(user.ProjectTasks);
                foreach (ProjectTask task in sortedTasks)
                {
                    Printer.PrintTask(task, indentLevel: 1);
                }
            }
            Console.ReadKey();
        }
       
    }
}