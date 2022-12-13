namespace TaskManager.Application.Exceptions;

public class TaskListNotFoundException : Exception
{
    public TaskListNotFoundException(Guid id)
        : base($"TaskList not found with Id : {id}")
    {
    }
}