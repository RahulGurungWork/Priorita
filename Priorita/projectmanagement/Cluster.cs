using Priorita.projectmanagement;

namespace Priorita.ProjectManagement
{
    public class Cluster
    {
        public string Name { get; set; } = "New Cluster";
        public string Description { get; set; } = "No Description";
        public List<Project> Projects { get; set; } = new List<Project>();

        public void AddProject(Project project)
        {
            Projects.Add(project);
            Console.WriteLine($"Project '{project.Name}' linked to Cluster '{Name}'.");
        }

        public void RemoveProject(Project project)
        {
            if (Projects.Remove(project))
            {
                Console.WriteLine($"Project '{project.Name}' unlinked from Cluster '{Name}'.");
            }
            else
            {
                Console.WriteLine($"Project '{project.Name}' not found in this cluster.");
            }
        }

        public void DisplayClusterSummary()
        {
            int totalTasks = Projects.Sum(project => project.Tasks.Count);
            int highPriorityTasks = Projects.Sum(project => project.GetTasksByQuadrant(MatrixQuadrant.DoFirst).Count);

            Console.WriteLine($"=== CLUSTER: {Name} ===");
            Console.WriteLine($"Projects Included: {Projects.Count}");
            Console.WriteLine($"Total Tasks Across Projects: {totalTasks}");
            Console.WriteLine($"CRITICAL Tasks (DoFirst): {highPriorityTasks}");
            Console.WriteLine("========================");
        }
    }
}