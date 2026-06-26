using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Infrastructure.Repositories;

public class LiteDbTaskRepository : ITaskRepository
{

    private readonly string _connectionString = @"Filename=TaskTracker.db;Connection=shared";
    private const string CollectionName = "tasks";

    private ILiteCollection<TaskItem> GetCollection(LiteDatabase db)
            => db.GetCollection<TaskItem>(CollectionName);

    public Task AddAsync(TaskItem task)
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = GetCollection(db);
            collection.Insert(task); 
        }
        return Task.CompletedTask;
    }
    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = GetCollection(db);
            var task = collection.FindById(id);
            return Task.FromResult<TaskItem?>(task);
        }
    }
    public Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = GetCollection(db);
            var tasks = collection.FindAll();
            return Task.FromResult<IEnumerable<TaskItem>>(tasks);
        }
    }
    public Task UpdateAsync(TaskItem task)
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = GetCollection(db);
            collection.Update(task);
        }
        return Task.CompletedTask;
    }
    public Task DeleteAsync(Guid id)
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = GetCollection(db);
            collection.Delete(id);
        }
        return Task.CompletedTask;
    }
}
