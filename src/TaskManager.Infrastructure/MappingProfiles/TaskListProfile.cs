using AutoMapper;
using TaskManager.Application.Models.Dto;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.MappingProfiles;

public class TaskListProfile : Profile
{
    public TaskListProfile()
    {
        this.CreateMap<TaskList, TaskListDto>();
    }
}