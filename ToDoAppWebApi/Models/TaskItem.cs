namespace ToDoAppWebApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Detail { get; set; }

        public bool IsCompleted { get; set; } = false;

        public string? Priority { get; set; }  // "Low", "Medium", "High"

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int UserId { get; set; } 

    }
}
