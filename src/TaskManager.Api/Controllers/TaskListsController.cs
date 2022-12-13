using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Application.CommandHandlers;
using TaskManager.Application.QueryHandlers;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/task-lists/")]
public class TaskListsController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public TaskListsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTaskList(Guid id)
    {
        return Ok(await _mediator.Send(new GetTaskListQuery(id)));
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTaskList(Guid id, [FromBody] Guid currentUserId)
    {
        return Ok(await _mediator.Send(new DeleteTaskListCommand(id, currentUserId)));
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateTaskList(Guid id, [FromBody] string name)
    {
        return Ok(await _mediator.Send(new UpdateTaskListCommand(id, name)));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListModel model)
    {
        return Ok(await _mediator.Send(new CreateTaskListCommand(model.Name, model.OwnerId)));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTaskLists()
    {
        return Ok(await _mediator.Send(new GetAllTaskListsQuery()));
    }
    
    /// <summary>
    /// Share TaskList with specific user
    /// </summary>
    [HttpPost]
    [Route("{id}/share/{userId}")]
    public async Task<IActionResult> ShareTaskList(Guid id, Guid userId, [FromBody]Guid currentUserId)
    {
        return Ok(await _mediator.Send(new ShareTaskListCommand(id, userId, currentUserId)));
    }
    
    /// <summary>
    /// Revoke user access to TaskList
    /// </summary>
    [HttpDelete]
    [Route("{id}/share/{userId}")]
    public async Task<IActionResult> RevokeUserAccessToTaskList(Guid id, Guid userId, [FromBody] Guid currentUserId)
    {
        return Ok(await _mediator.Send(new RevokeUserAccessToTaskListCommand(id, userId, currentUserId)));
    }
    
    /// <summary>
    /// Get list of task list with all connected users
    /// </summary>
    [HttpGet]
    [Route("list/{currentUserId}")]
    public async Task<IActionResult> GetListOfTaskLists(Guid currentUserId, [FromQuery] PaginationModel paging)
    {
        return Ok(await _mediator.Send(new GetTaskListWithSharedUsersQuery(currentUserId, paging.Skip, paging.Take)));
    }
}