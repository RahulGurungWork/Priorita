//-----------------------------------------------------------------------
// <copyright file="Cluster.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------


using Priorita.utilities;

namespace Priorita.projectmanagement
{
    public class Cluster
    {
        /// <summary>
        /// Default variables for new clusters
        /// </summary>
        public string Name { get; set; } = "New Cluster";
        public string Description { get; set; } = "No Description";
        public List<Project> Projects { get; set; } = [];

        /// <summary>
        /// Adds a project to the cluster and logs the association.
        /// </summary>
        /// <param name="project">The project to add to the cluster. Cannot be <c>null</c>.</param>
        public void AddProject(Project project)
        {
            Logger.Log(LogCategory.CLUSTER, $"Linked Project '{project.Name}' to Cluster '{Name}'.");
            Projects.Add(project);
        }
    }
}