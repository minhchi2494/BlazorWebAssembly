using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TodoList.Api.Data;
using Microsoft.EntityFrameworkCore;
using Task = TodoList.Api.Entities.Task;
using TodoList.Models;

namespace TodoList.Api.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public TodoListDbContext _context; 

        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Task>> GetTaskList()
        {
            return await _context.Tasks.Include(x=>x.Assignee).ToListAsync();
        }

        public async Task<Entities.Task> Create(Task task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;

        }

        public async Task<Entities.Task> Delete(Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Entities.Task> Update(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
