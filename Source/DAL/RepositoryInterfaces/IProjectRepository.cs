using DAL.Entities;

namespace DAL.RepositoryInterfaces
{
    public interface IProjectRepository
    {
        Task<bool> PersonByIdExistsAsync(int id, CancellationToken cancellation);
        Task<bool> SaveChangesAsync(CancellationToken cancellation);
        //Task<bool> ProjectByTitleExistsAsync(string title, CancellationToken cancellation);
        Task<Project?> GetProjectAsync(int projectId, CancellationToken cancellation);
        Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken cancellation);
        Task<IEnumerable<Project>> GetDeletedProjectsForPersonAsync(int personId, CancellationToken cancellation);
        Task CreateProject(int personId, Project project, CancellationToken cancellation);
        void DeleteProject(int id);             
        void RetrieveProject(int id);
    }
}
