namespace TaskManager.Domain;

public class Task
{
    public Guid Id { get; init; }
    public Guid TaskListId { get; init; }
    public string Description { get; init; }
    public bool IsCompleted { get; init; }
    public virtual TaskList TaskList { get; init; }
}