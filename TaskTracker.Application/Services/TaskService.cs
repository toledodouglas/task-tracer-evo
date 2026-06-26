using System;
using System.Threading.Tasks;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }
    public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
    {   
        if(id == Guid.Empty)
            throw new ArgumentException("The task ID cannot be empty.");
        return await _taskRepository.GetByIdAsync(id);
    }
    public async Task<TaskItem> CreateTaskAsync(string title, string description, List<string> tags)
    {
        var task = new TaskItem(title, description, tags);
        await _taskRepository.AddAsync(task);
        return task;
    }
    public async Task MoveToInProgressAsync(Guid Id)
    {
        var task = await _taskRepository.GetByIdAsync(Id);
        if (task == null)
            throw new Exception("Task not found");
        task.MoveToInProgress();
        await _taskRepository.UpdateAsync(task);
    }
    public async Task MoveToDoneAsync(Guid Id)
    {
        var task = await _taskRepository.GetByIdAsync(Id);
        if (task == null)
            throw new Exception("Task not found");
        task.MoveToDone();
        await _taskRepository.UpdateAsync(task);
    }
    public async Task DeleteTaskAsync(Guid Id)
    {
        var task = await _taskRepository.GetByIdAsync(Id);
        if (task == null)
            throw new Exception("Task not found");
        await _taskRepository.DeleteAsync(Id);
    }
    public async Task UpdateTaskAsync(Guid Id, string title, string description, List<string> tags)
    {
        var task = await _taskRepository.GetByIdAsync(Id);
        if (task == null)
            throw new Exception("Task not found");
        task.Update(title, description, tags);
        await _taskRepository.UpdateAsync(task);
    }

}
