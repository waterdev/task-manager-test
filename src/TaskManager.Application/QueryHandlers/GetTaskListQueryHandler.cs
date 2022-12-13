using AutoMapper;
using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Models.Dto;
using TaskManager.Domain;

namespace TaskManager.Application.QueryHandlers;

public record GetTaskListQuery(Guid Id) : IRequest<TaskListDto>;

public class GetTaskListQueryHandler: IRequestHandler<GetTaskListQuery, TaskListDto>
{
    private readonly IRepository<TaskList> _repository;
    private readonly IMapper _mapper;
    public GetTaskListQueryHandler(IRepository<TaskList> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<TaskListDto> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
    {
        var taskList = await _repository.GetById(request.Id);

        if (taskList is null)
        {
            throw new TaskListNotFoundException(request.Id);
        }

        return _mapper.Map<TaskListDto>(taskList);
    }
}