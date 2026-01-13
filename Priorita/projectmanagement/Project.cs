//-----------------------------------------------------------------------
// <copyright file="Project.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------

using Priorita.utilities;

namespace Priorita.projectmanagement
{
    /// <summary>
    /// Project class representing a collection of tasks.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Default variables for new projects
        /// </summary>
        public string Name { get; set; } = "New Project";
        public string Description { get; set; } = "No Description";
        public DateTime StartTime { get; set; } = DateTime.Now;
        public List<ProjectTask> Tasks { get; set; } = [];


        /// <summary>
        /// Adds a task to the project and logs the addition.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(ProjectTask task)
        {
            Tasks.Add(task);
            Logger.Log(LogCategory.TASK, $"Task '{task.Title}' added to project '{Name}'.");
        }
    }
}