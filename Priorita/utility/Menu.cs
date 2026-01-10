namespace Priorita.utility
{
    internal class Menu
    {
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
                    0) Exit
                    """;
        }
    }
}
