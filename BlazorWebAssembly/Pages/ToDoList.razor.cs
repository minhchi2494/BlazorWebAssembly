using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using BlazorWebAssembly.Services;
using TodoList.Models;

namespace BlazorWebAssembly.Pages
{
    public partial class ToDoList
    {
        [Inject] private ITaskApiClient services { get; set; }

        private List<TaskDto> Tasks;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await services.GetTaskList();
        }
    }
}
