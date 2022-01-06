using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models;

namespace BlazorWebAssembly.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>> GetTaskList();
    }
}
