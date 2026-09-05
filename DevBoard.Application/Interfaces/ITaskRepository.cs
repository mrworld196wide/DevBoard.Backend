using DevBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        Task SaveChangesAsync();
    }
}
