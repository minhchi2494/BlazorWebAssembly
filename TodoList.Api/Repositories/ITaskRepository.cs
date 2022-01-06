using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Task = TodoList.Api.Entities.Task;
using TodoList.Models;

namespace TodoList.Api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList();
        Task<Task> Create(Task task);
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}
