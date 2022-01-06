using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetTaskList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/Tasks");
            return result;
        }
    }
}
