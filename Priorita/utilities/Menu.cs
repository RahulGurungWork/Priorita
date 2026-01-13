//-----------------------------------------------------------------------
// <copyright file="Menu.cs" company="GIBB">
//      Copyright (c) GIBB. All rights reserved.
// </copyright>
// <author>Rahul Gurung</author>
// <date>2026-01-13</date>
// <summary>Entry of the Program.</summary>
//-----------------------------------------------------------------------


namespace Priorita.utilities
{
    /// <summary>
    /// A class representing the main menu of the application.
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// A method that returns the options available in the main menu.
        /// </summary>
        /// <returns>A string of options for the main menu</returns>
        public static string GetMainMenu()
        { 
            return """
                    1) Create Task
                    2) Create Project
                    3) Create Cluster
                    4) Create User 
                    5) Add Task to Project 
                    6) Add Project to Cluster
                    7) Change Users
                    8) View Board
                    0) Exit
                    """;
        }
    }
}
