using DevBoard.Application.DTOs;
using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using DevBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IEventPublisher _publisher;

        public TaskService(ITaskRepository taskRepo, IEventPublisher publisher)
        {
            _taskRepo = taskRepo;
            _publisher = publisher;
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                Status = Domain.Enums.TaskStatus.Todo
            };

            await _taskRepo.AddAsync(task);
            await _taskRepo.SaveChangesAsync();

            await _publisher.PublishAsync("task.created", new { task.Id, task.Title });

            return new TaskDto(task.Id, task.Title, task.Description, task.Status.ToString(), task.ProjectId);
        }
    }
}
