using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.CommandHandlers;

public record DeleteTaskListCommand(Guid Id, Guid CurrentUserId) : IRequest;

public class DeleteTaskListHandler: IRequestHandler<DeleteTaskListCommand, Unit>
{
    private readonly IRepository<TaskList> _repository;
    public DeleteTaskListHandler(IRepository<TaskList> repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _repository.GetById(request.Id);

        if (taskList is null)
        {
            throw new TaskListNotFoundException(request.Id);
        }

        if (taskList.OwnerId != request.CurrentUserId)
        {
            throw new NoPermissionException($"User with id {request.CurrentUserId} has no permission to delete TaskList with id {request.Id}");
        }

        _repository.Remove(taskList);
        await _repository.SaveChanges();
        
        return Unit.Value;
    }
}