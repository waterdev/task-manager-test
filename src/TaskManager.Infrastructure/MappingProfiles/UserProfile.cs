using AutoMapper;
using TaskManager.Application.Models.Dto;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.MappingProfiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        this.CreateMap<User, UserDto>();
    }
}