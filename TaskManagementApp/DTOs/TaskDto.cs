namespace TaskManagementApp.DTOs
{
    public class TaskDto
    {
        public int TaskId { get; set; }

        public string TaskDescription { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly? ExpectedClosureDate { get; set; }

        public string? AssignedTo { get; set; }

        public string? CompletionStatus { get; set; }
    }
}
