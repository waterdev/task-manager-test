namespace TaskManager.Domain;

public record User
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    
    public virtual ICollection<User> TaskLists { get; init; }
}