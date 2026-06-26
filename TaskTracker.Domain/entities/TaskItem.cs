using System;
using System.Collections.Generic;
using TaskTracker.Domain.Enums;

namespace TaskTracker.Domain.Entities;

public class TaskItem : Entity
{
	public string Title { get; private set; }
	public string Description { get; private set; }
	public TasksStatus Status { get; private set; }
	public List<string> Tags { get; private set; }
	public DateTime CreatedAt { get; private set; }

    protected TaskItem() { }
    public TaskItem(string title, string description,List<string> tags)
	{
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("The task title cannot be empty.");

        Title = title;
		Description = description;
		Tags = tags ?? new List<string>();

        Status = TasksStatus.Todo;
		CreatedAt = DateTime.UtcNow;

	}

    public void Update(string title, string description, List<string> tags)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("The task title cannot be empty.");

        Title = title;
        Description = description;
        Tags = tags ?? new List<string>();
    }

    public void MoveToInProgress()
    {
        if(Status == TasksStatus.Done)
            throw new InvalidOperationException("It is not possible to move a completed task to 'In Progress'.");
        Status = TasksStatus.InProgress;
    }
    public void MoveToDone()
    {
        Status = TasksStatus.Done;
    }

}