using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.CommandHandlers;

public record RevokeUserAccessToTaskListCommand(Guid Id, Guid UserId, Guid CurrentUserId) : IRequest;

public class RevokeUserAccessToTaskListHandler : IRequestHandler<RevokeUserAccessToTaskListCommand, Unit>
{
    private readonly IRepository<TaskList> _taskListRepo;

    public RevokeUserAccessToTaskListHandler(IRepository<TaskList> taskListRepo)
    {
        _taskListRepo = taskListRepo;
    }

    public async Task<Unit> Handle(RevokeUserAccessToTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _taskListRepo.GetById(request.Id);
        
        if (taskList is null)
        {
            throw new TaskListNotFoundException(request.Id);
        }
        
        var user = taskList.SharedWith.FirstOrDefault(x => x.Id == request.CurrentUserId);

        if (taskList.OwnerId != request.CurrentUserId && user is null)
        {
            throw new NoPermissionException($"User with id {request.CurrentUserId} has no permission to revoke access to  TaskList with id {request.Id}");
        }
        
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        taskList.SharedWith.Remove(user);

        await _taskListRepo.SaveChanges();

        return Unit.Value;
    }
}