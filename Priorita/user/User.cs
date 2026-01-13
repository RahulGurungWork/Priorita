//-----------------------------------------------------------------------
// <copyright file="User.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------



using Priorita.projectmanagement;
using Priorita.utilities;

namespace Priorita.users
{
    /// <summary>
    /// A class representing a user in the project management system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Default Values for some variables for new 
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Cluster> Clusters { get; set; } = new List<Cluster>();
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

        /// <summary>
        /// Constructor to initialize a User instance with a username and password.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public User(string name, string password)
        {
            Username = name;
            Password = password;
            Logger.Log(LogCategory.SYSTEM, $"User instance initialized for: {Username}");
        }

        /// <summary>
        /// Lists all project tasks associated with the user.
        /// </summary>
        public void ListProjectTasks()
        {
            Logger.Log(LogCategory.INFO, $"User '{Username}' viewed personal Task list.");
            if (ProjectTasks.Count == 0)
            {
                Printer.PrintInformation("No project tasks available.");
                return;
            }

            Printer.PrintHeader($"Project Tasks for User: {Username}");
            for(int i = 0; i < ProjectTasks.Count; i++)
            {
                var task = ProjectTasks[i];
                Console.WriteLine($"{i}) {task.Title} - Quadrant: {task.Quadrant}");
            }
        }
        /// <summary>
        /// Lists all projects associated with the user.
        /// </summary>
        public void ListProjects()
        {
            Logger.Log(LogCategory.INFO, $"User '{Username}' viewed Project list.");
            if (Projects.Count == 0)
            {
                Printer.PrintInformation("No projects available.");
                return;
            }

            Printer.PrintHeader($"=== Projects for User: {Username} ===");
            for(int i = 0; i < Projects.Count; i++)
            {
                var project = Projects[i];
                Console.WriteLine($"{i}) {project.Name} (Tasks: {project.Tasks.Count})");
            }
        }
        /// <summary>
        /// Lists all clusters associated with the user.
        /// </summary>
        public void ListClusters()
        {
            Logger.Log(LogCategory.INFO, $"User '{Username}' viewed Cluster list.");
            if (Clusters.Count == 0)
            {
                Printer.PrintInformation("No clusters available.");
                return;
            }

            Printer.PrintHeader($"=== Clusters for User: {Username} ===");
                for(int i = 0; i < Clusters.Count; i++)
                {
                    var cluster = Clusters[i];
                    Console.WriteLine($"{i}) {cluster.Name} (Projects: {cluster.Projects.Count})");
                }
            }

        /// <summary>
        /// Adds a project to the user's root project list and logs the addition.
        /// </summary>
        /// <param name="project"></param>
        public void AddProject(Project project)
        {
            Projects.Add(project);
            Logger.Log(LogCategory.PROJECT, $"Project '{project.Name}' added to user '{Username}' root list.");
        }

        /// <summary>
        /// Adds a cluster to the user's root cluster list and logs the addition.
        /// </summary>
        /// <param name="cluster"></param>
        public void AddCluster(Cluster cluster)
        {
            Clusters.Add(cluster);
            Logger.Log(LogCategory.CLUSTER, $"Cluster '{cluster.Name}' added to user '{Username}' root list.");
        }
    }
}