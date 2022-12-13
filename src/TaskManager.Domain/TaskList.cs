namespace TaskManager.Domain;

public class TaskList
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid OwnerId { get; init; }
    
    public DateTime CreatedAt { get; init; }
    public virtual ICollection<Task> Tasks { get; init; }

    public virtual ICollection<User> SharedWith { get; init; }
}
