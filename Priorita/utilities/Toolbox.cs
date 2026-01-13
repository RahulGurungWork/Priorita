//-----------------------------------------------------------------------
// <copyright file="Toolbox.cs" company="GIBB">
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
    public class Toolbox
    {
        /// <summary>
        /// Sorts a list of ProjectTask objects by their priority based on MatrixQuadrant and TimeCriticality.
        /// </summary>
        /// <returns>A sorted List of ProjectTask Objects</returns>
        public static List<ProjectTask> SortTasksByPriority(List<ProjectTask> tasks) => tasks.OrderBy(task => task.Quadrant).ThenBy(task => task.TimeCriticality).ToList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUserList()
        {
            List<ProjectTask> tasks1 = [];
            tasks1.Add(new ProjectTask { Title = "Task 1", Description = "Important and Urgent", StrategicValue = 9, Deadline = DateTime.Now.AddDays(2) });
            tasks1.Add(new ProjectTask { Title = "Task 2", Description = "Important but Not Urgent", StrategicValue = 8, Deadline = DateTime.Now.AddDays(10) });
            tasks1.Add(new ProjectTask { Title = "Task 3", Description = "Not Important but Urgent", StrategicValue = 4, Deadline = DateTime.Now.AddDays(1) });

            List<ProjectTask> tasks2 = [];
            tasks2.Add(new ProjectTask { Title = "Task A", Description = "Important and Urgent", StrategicValue = 10, Deadline = DateTime.Now.AddDays(3) });
            tasks2.Add(new ProjectTask { Title = "Task B", Description = "Not Important and Not Urgent", StrategicValue = 2, Deadline = DateTime.Now.AddDays(15) });
            tasks2.Add(new ProjectTask { Title = "Task C", Description = "Important but Not Urgent", StrategicValue = 7, Deadline = DateTime.Now.AddDays(7) });

            List<ProjectTask> tasks3 = [];
            tasks3.Add(new ProjectTask { Title = "Task X", Description = "Not Important but Urgent", StrategicValue = 3, Deadline = DateTime.Now.AddDays(2) });
            tasks3.Add(new ProjectTask { Title = "Task Y", Description = "Important and Urgent", StrategicValue = 9, Deadline = DateTime.Now.AddDays(1) });
            tasks3.Add(new ProjectTask { Title = "Task Z", Description = "Not Important and Not Urgent", StrategicValue = 1, Deadline = DateTime.Now.AddDays(20) });

            List<Project> project1 = [];
            project1.Add(new Project { Name = "Project Alpha", Description = "First Project", StartTime = DateTime.Now, Tasks = tasks1 });
            List<Project> project2 = [];
            project2.Add(new Project { Name = "Project Beta", Description = "Second Project", StartTime = DateTime.Now, Tasks = tasks2 });
            List<Project> project3 = [];
            project3.Add(new Project { Name = "Project Gamma", Description = "Third Project", StartTime = DateTime.Now, Tasks = tasks3 });

            Cluster cluster1 = new Cluster { Name = "Cluster One", Description = "First Cluster", Projects = project1 };
            Cluster cluster2 = new Cluster { Name = "Cluster Two", Description = "Second Cluster", Projects = project2 };

            List<User> users = [];
            User user1 = new User("alice", "password123");
            user1.Projects = project1;
            user1.Clusters.Add(cluster1);
            user1.ProjectTasks = tasks1;
            users.Add(user1);

            User user2 = new User("bob", "securepass");
            user2.Projects = project2;
            user2.Clusters.Add(cluster2);
            user2.ProjectTasks = tasks2;
            users.Add(user2);

            return users;

        }
    }
}
