using DAL.Data;
using DAL.Entities;
using DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CVContext _context;

        public ProjectRepository(CVContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<bool> PersonByIdExistsAsync(int personId, CancellationToken cancellation)
        {
            return await _context.People.AnyAsync(p => p.Id == personId && p.IsDeleted == false, cancellation);
        }

        public async Task<Project?> GetProjectAsync(int projectId, CancellationToken cancellation)
        {
            return await _context.Projects
                .Where(p => p.Id == projectId && p.IsDeleted == false)
                .Include(p => p.Skills).Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellation)//bool voor includeDeleted
        {
            return await _context.Projects.ToListAsync(cancellation);
        }

        public async Task<IEnumerable<Project>> GetDeletedProjectsForPersonAsync(int personId, CancellationToken cancellation)
        {
            return await _context.People
                                .Where(p => p.Id == personId)
                                .Include(p => p.Projects)
                                .SelectMany(p => p.Projects.Where(p => p.IsDeleted == true))
                                .ToListAsync(cancellation);                
        }

        public async Task CreateProject(int personId, Project project, CancellationToken cancellation)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.Id == personId, cancellation);
            person?.Projects.Add(project);                         
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellation)
        {
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public void DeleteProject(int id)
        {
            _context.Projects.Where(p => p.Id == id).First().IsDeleted = true;
        }

        public void RetrieveProject(int id)
        {
            _context.Projects.Where(p => p.Id == id).First().IsDeleted = false;
        }
    }
}
