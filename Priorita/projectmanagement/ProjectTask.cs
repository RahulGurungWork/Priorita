//-----------------------------------------------------------------------
// <copyright file="ProjectTask.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------


namespace Priorita.projectmanagement
{
    /// <summary>
    /// A class representing a task within a project, including its strategic value, deadline, and calculated priority quadrant.
    /// </summary>

    public class ProjectTask
    {

        /// <summary>
        /// Default Values for some variables for new tasks
        /// </summary>
        public string Title { get; set; } = "New Task";
        public string Description { get; set; } = "No Description";

        // StrategicValue: 1 (Low) to 10 (High)
        public int StrategicValue { get; set; } = 5;
        public DateTime Deadline { get; set; } = DateTime.Now;
        public int TimeCriticality
        {
            get
            {
                return (Deadline - DateTime.Now).Days;
            }
        }
        public MatrixQuadrant Quadrant
        {
            get
            {
                return CalculateQuadrant();
            }
        }

        /// <summary>
        /// Calculates the priority quadrant of the task based on its strategic value and time criticality.
        /// </summary>
        /// <returns>A Matrix Quadrabt Enum value</returns>
        private MatrixQuadrant CalculateQuadrant()
        { 
            bool isUrgent = TimeCriticality <= 3;
            bool isImportant = StrategicValue > 5;

            if (isUrgent && isImportant) return MatrixQuadrant.DoFirst;
            if (!isUrgent && isImportant) return MatrixQuadrant.Schedule;
            if (isUrgent && !isImportant) return MatrixQuadrant.Delegate;
            return MatrixQuadrant.Eliminate;
        }
        /// <summary>
        /// Defines a label for each quadrant for easy identification.
        /// </summary>
        /// <returns>A string representing a quadrant</returns>
        public string GetQuadrantLabel()
        {
            return Quadrant switch
            {
                MatrixQuadrant.DoFirst => "[!]",
                MatrixQuadrant.Schedule => "[S]",
                MatrixQuadrant.Delegate => "[D]",
                MatrixQuadrant.Eliminate => "[X]",
                _ => "[?]"
            };
        }
    }
}