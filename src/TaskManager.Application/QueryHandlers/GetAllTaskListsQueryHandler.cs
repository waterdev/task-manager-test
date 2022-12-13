using AutoMapper;
using MediatR;
using TaskManager.Application.Models.Dto;
using TaskManager.Domain;

namespace TaskManager.Application.QueryHandlers;

public record GetAllTaskListsQuery : IRequest<IEnumerable<TaskListDto>>;

public class GetAllTaskListsQueryHandler: IRequestHandler<GetAllTaskListsQuery, IEnumerable<TaskListDto>>
{
    private readonly IRepository<TaskList> _repository;
    private readonly IMapper _mapper;
    public GetAllTaskListsQueryHandler(IRepository<TaskList> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TaskListDto>> Handle(GetAllTaskListsQuery request, CancellationToken cancellationToken)
    {
        return (await _repository.GetAll()).Select(x=> _mapper.Map<TaskListDto>(x));
    }
}