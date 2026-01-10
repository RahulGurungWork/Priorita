using Priorita.projectmanagement;
using System.Text.Json;

namespace Priorita.ProjectManagement
{
    public class Project
    {
        public string Name { get; set; } = "New Project";
        public DateTime StartTime { get; set; } = DateTime.Now;
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        public void ExportTasks(string filePath = "project_export.json")
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(Tasks, options);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Successfully exported {Tasks.Count} tasks to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export failed: {ex.Message}");
            }
        }

        public void AddTask(ProjectTask task)
        {
            Tasks.Add(task);
            Console.WriteLine($"Task '{task.Title}' added to project '{Name}'.");
        }

        public void RemoveTask(ProjectTask task)
        {
            bool removed = Tasks.Remove(task);

            if (removed)
            {
                Priorita.Utility.Logger.Log("TASK_REMOVE", $"Removed task: {task.Title}");
            }
            else
            {
                Console.WriteLine($"Task '{task.Title}' could not be found in the list.");
            }
        }
        public List<ProjectTask> GetTasksByQuadrant(MatrixQuadrant quadrant)
        {
            return Tasks.Where(task => task.Quadrant == quadrant).ToList();
        }
    }
}