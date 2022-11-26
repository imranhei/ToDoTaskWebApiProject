namespace TaskWebApi.Model
{
    public class TaskModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Breakfast { get; set; } = string.Empty;
        public string Brunch { get; set; } = string.Empty;
        public string Lunch { get; set; } = string.Empty;
        public string Supper { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
    }
}
