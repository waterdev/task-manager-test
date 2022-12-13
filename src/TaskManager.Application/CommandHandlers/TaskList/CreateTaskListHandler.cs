using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.CommandHandlers;

public record CreateTaskListCommand(string Name, Guid OwnerId) : IRequest;

public class CreateTaskListHandler: IRequestHandler<CreateTaskListCommand, Unit>
{
    private readonly IRepository<TaskList> _repository;
    public CreateTaskListHandler(IRepository<TaskList> repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
    {
        var newTaskList = new TaskList()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            OwnerId = request.OwnerId
        };
        
        _repository.Add(newTaskList);
        await _repository.SaveChanges();
        
        return Unit.Value;
    }
}