namespace TaskManager.Models;

// Represents a single task in the system
public class TaskItem
{
    // Unique task ID
    public int Id { get; set; }
    // Short title for the task
    public string Title { get; set; } = string.Empty;
    // Optional detailed description
    public string? Description { get; set; }
    // Due date for the task
    public DateTime DueDate { get; set; }
    // Whether the task has been completed
    public bool IsCompleted { get; set; }
    // Priority level: Low, Medium, or High
    public string Priority { get; set; } = "Medium";
}
