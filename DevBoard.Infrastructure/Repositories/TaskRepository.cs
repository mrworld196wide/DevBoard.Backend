using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using DevBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevBoard.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) => _context = context;

        public async Task<TaskItem?> GetByIdAsync(Guid id) =>
            await _context.Tasks.FindAsync(id);

        public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId) =>
            await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();

        public async Task AddAsync(TaskItem task) =>
            await _context.Tasks.AddAsync(task);

        public void Update(TaskItem task) =>
            _context.Tasks.Update(task);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
