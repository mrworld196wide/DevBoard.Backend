using DevBoard.Application.DTOs;
using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo) => _projectRepo = projectRepo;

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto, Guid ownerId)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = ownerId
            };

            await _projectRepo.AddAsync(project);
            await _projectRepo.SaveChangesAsync();

            return new ProjectDto(project.Id, project.Name, project.Description);
        }

        public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(Guid userId)
        {
            var projects = await _projectRepo.GetAllForUserAsync(userId);
            return projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description));
        }
    }
}
