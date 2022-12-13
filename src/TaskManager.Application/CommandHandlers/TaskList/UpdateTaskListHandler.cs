using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.CommandHandlers;

public record UpdateTaskListCommand(Guid Id, string Name) : IRequest;

public class UpdateTaskListHandler: IRequestHandler<UpdateTaskListCommand, Unit>
{
    private readonly IRepository<TaskList> _repository;
    public UpdateTaskListHandler(IRepository<TaskList> repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(UpdateTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _repository.GetById(request.Id);

        if (taskList is null)
        {
            throw new TaskListNotFoundException(request.Id);
        }

        taskList.Name = request.Name;

        _repository.Update(taskList);
        await _repository.SaveChanges();
        
        return Unit.Value;
    }
}