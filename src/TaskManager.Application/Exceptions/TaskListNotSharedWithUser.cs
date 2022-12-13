namespace TaskManager.Application.Exceptions;

public class TaskListNotSharedWithUser: Exception
{
    public TaskListNotSharedWithUser(Guid id, Guid userId)
        : base($"TaskList with Id : {id} is not shared with User with id {userId} ")
    {
    }
}
