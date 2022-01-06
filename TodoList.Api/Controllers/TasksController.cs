using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Task = TodoList.Api.Entities.Task;
using TodoList.Api.Repositories;
using TodoList.Models;
using TodoList.Models.Enums;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _services;

        public TasksController(ITaskRepository services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _services.GetTaskList();
            var taskDtos = tasks.Select(x => new TaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priotiry = x.Priotiry,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A",
            });
            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = await _services.Create(new Task()
            {
                Name = request.Name,
                Priotiry = request.Priotiry,
                Status = Status.Done,
                CreatedDate = DateTime.Now,
                Id = request.Id
            });
            return Ok(model);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id,TaskUpdateRequest request)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var taskfromDb = await _services.GetById(id);
            if (taskfromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskfromDb.Name = request.Name; 
            taskfromDb.Priotiry = request.Priotiry;
            var taskResult = await _services.Update(taskfromDb);
            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priotiry = taskResult.Priotiry,
                CreatedDate = taskResult.CreatedDate,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _services.GetById(id);
            if (task == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priotiry = task.Priotiry,
                CreatedDate = task.CreatedDate,
            });
        }
    }
}
