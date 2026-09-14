using DevBoard.Application.DTOs;
using DevBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        { 
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
        {
            // TODO: replace with actual logged-in user id once auth (Phase E) is done
            var fakeOwnerId = Guid.NewGuid();
            var result = await _projectService.CreateProjectAsync(dto, fakeOwnerId);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll([FromQuery] Guid userId)
        {
            var result = await _projectService.GetUserProjectsAsync(userId);
            return Ok(result);
        }
    }
}
