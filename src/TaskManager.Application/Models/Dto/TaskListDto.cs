namespace TaskManager.Application.Models.Dto;

public class TaskListDto
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    
    public Guid OwnerId { get; init; }
}