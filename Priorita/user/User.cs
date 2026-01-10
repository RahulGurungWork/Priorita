using Priorita.ProjectManagement;
using Priorita.utility;
using System.Collections.Generic;

namespace Priorita.Users 
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Cluster> Clusters { get; set; } = new List<Cluster>();
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

        public User(string name, string password)
        {
            Username = name;
            Password = password;
        }
        public void ListProjectTasks()
        {
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
        public void ListProjects()
        {
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

        public void ListClusters()
        {
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

        public void AddProject(Project project)
        {
            Projects.Add(project);
        }

        public void AddCluster(Cluster cluster)
        {
            Clusters.Add(cluster);
        }
    }
}