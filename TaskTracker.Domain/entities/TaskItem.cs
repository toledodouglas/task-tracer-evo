using System;

public class TaskItem : Entity
{
	public string Title { get; private set; }
	public string Description { get; private set; }
	public string Status { get; private set; }
	public list<string> Tag { get; private set; }
	public DateTime CreatedAt { get; private set; }
	public TaskItem(string title, string description, string status, list<string> tag, DateTime createdAt)
	{
		Title = title;
		Description = description;
		Status = status;
		Tag = tag;
		DueDate = createdAt;

	}

}