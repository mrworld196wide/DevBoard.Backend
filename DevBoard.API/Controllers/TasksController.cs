using DevBoard.Application.DTOs;
using DevBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService) 
        { 
            _taskService = taskService; 
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create(CreateTaskDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
    }
}
