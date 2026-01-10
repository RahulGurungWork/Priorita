using Priorita.projectmanagement;

namespace Priorita.ProjectManagement
{

    public class ProjectTask
    {
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
        private MatrixQuadrant CalculateQuadrant()
        { 
            bool isUrgent = TimeCriticality <= 3;
            bool isImportant = StrategicValue > 5;

            if (isUrgent && isImportant) return MatrixQuadrant.DoFirst;
            if (!isUrgent && isImportant) return MatrixQuadrant.Schedule;
            if (isUrgent && !isImportant) return MatrixQuadrant.Delegate;
            return MatrixQuadrant.Eliminate;
        }
        public void Display()
        {
            Utility.Logger.Log("TASK_VIEW", $"Displayed task details for: {Title}");
            Console.WriteLine($"[{Quadrant}] {Title}");
            Console.WriteLine($"   Due in: {TimeCriticality} days | Value: {StrategicValue}/10");
            Console.WriteLine($"   Desc: {Description}");
            Console.WriteLine("---------------------------------------------");
        }
    }
}