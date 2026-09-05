using System;
using DevBoard.Domain.Enums;
using TaskStatus = DevBoard.Domain.Enums.TaskStatus;
namespace DevBoard.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public Guid ProjectId { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}
