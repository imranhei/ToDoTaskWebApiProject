namespace TaskWebApi.Model
{
    public class LogModel
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string MealTime { get; set; } = string.Empty;
        public string RequestedMeal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
