using System;
using System.Collections.Generic;
using TaskTracker.Domain.Enums;

namespace TaskTracker.Domain.Entities;

public class TaskItem : Entity
{
	public string Title { get; private set; }
	public string Description { get; private set; }
	public TaskStatus Status { get; private set; }
	public List<string> Tags { get; private set; }
	public DateTime CreatedAt { get; private set; }
	public TaskItem(string title, string description, TaskStatus status, List<string> tags, DateTime createdAt)
	{
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título da tarefa não pode ser vazio.");

        Title = title;
		Description = description;
        Status = TaskStatus.Todo;
		Tags = tags ?? new List<string>();
		CreatedAt = DateTime.UtcNow;

	}

}