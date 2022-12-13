namespace TaskManager.Application.Models.Dto;

public class TaskListWithUsersDto
{
    public TaskListDto TaskList { get; set; }
    
    public IEnumerable<UserDto> Users { get; set; }
}