using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;
    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        try
        {
            var task = await _taskService.CreateTaskAsync(request.Title, request.Description, request.Tags);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        try
        {
            await _taskService.UpdateTaskAsync(id, request.Title, request.Description, request.Tags);
            return NoContent();

        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/progress")]
    public async Task<IActionResult> MoveToInProgress(Guid id)
    {
        try
        {
            await _taskService.MoveToInProgressAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/done")]
    public async Task<IActionResult> MoveToDone(Guid id)
    {
        try
        {
            await _taskService.MoveToDoneAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

public record CreateTaskRequest(string Title, string Description, List<string> Tags);
public record UpdateTaskRequest(string Title, string Description, List<string> Tags);