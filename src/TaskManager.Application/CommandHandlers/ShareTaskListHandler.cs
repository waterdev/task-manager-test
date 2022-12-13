using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.CommandHandlers;

public record ShareTaskListCommand(Guid TaskListId, Guid UserId, Guid CurrentUserId) : IRequest;

public class ShareTaskListHandler: IRequestHandler<ShareTaskListCommand, Unit>
{
    private readonly IRepository<TaskList> _taskListRepo;
    public ShareTaskListHandler(IRepository<TaskList> taskListRepo)
    {
        _taskListRepo = taskListRepo;
    }
    
    public async Task<Unit> Handle(ShareTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _taskListRepo.GetById(request.TaskListId);
        
        if (taskList is null)
        {
            throw new TaskListNotFoundException(request.TaskListId);
        }
        
        var user = taskList.SharedWith.FirstOrDefault(x => x.Id == request.CurrentUserId);

        if (taskList.OwnerId != request.CurrentUserId && user is null)
        {
            throw new NoPermissionException($"User with id {request.CurrentUserId} has no permission to revoke access to  TaskList with id {request.TaskListId}");
        }

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        taskList.SharedWith.Add(user);
        
        await _taskListRepo.SaveChanges();
        
        return Unit.Value;
    }
}