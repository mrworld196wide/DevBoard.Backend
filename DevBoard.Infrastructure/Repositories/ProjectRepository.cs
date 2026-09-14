using DevBoard.Application.Interfaces;
using DevBoard.Domain.Entities;
using DevBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context) => _context = context;

        public async Task<Project?> GetByIdAsync(Guid id) =>
            await _context.Projects.FindAsync(id);

        public async Task<IEnumerable<Project>> GetAllForUserAsync(Guid userId) =>
            await _context.Projects.Where(p => p.OwnerId == userId).ToListAsync();

        public async Task AddAsync(Project project) =>
            await _context.Projects.AddAsync(project);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
