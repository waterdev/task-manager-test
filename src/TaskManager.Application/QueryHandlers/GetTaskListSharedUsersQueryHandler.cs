using AutoMapper;
using MediatR;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Models.Dto;
using TaskManager.Domain;

namespace TaskManager.Application.QueryHandlers;

public record GetTaskListWithSharedUsersQuery(Guid CurrentUserId, int Skip, int Take) : IRequest<IEnumerable<TaskListWithUsersDto>>;

public class GetTaskListSharedUsersQueryHandler : IRequestHandler<GetTaskListWithSharedUsersQuery, IEnumerable<TaskListWithUsersDto>>
{
    private readonly IRepository<TaskList> _repository;
    private readonly IMapper _mapper;

    public GetTaskListSharedUsersQueryHandler(IRepository<TaskList> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskListWithUsersDto>> Handle(GetTaskListWithSharedUsersQuery request, CancellationToken cancellationToken)
    {
        var listOfTaskLists = await _repository.GetAsync(x=> x.OwnerId == request.CurrentUserId 
                                                       || x.SharedWith.Any(y=> y.Id == request.CurrentUserId),
            orderBy: o => o.OrderByDescending(y=> y.CreatedAt),
            include: y=> y.SharedWith,
            request.Skip, request.Take);
        
        var result = listOfTaskLists.Select(x=>
        {
            return new TaskListWithUsersDto
            {
                TaskList = _mapper.Map<TaskListDto>(x),
                Users = x.SharedWith.Select(x => _mapper.Map<UserDto>(x))
            };
        });

        return result;
    }
}