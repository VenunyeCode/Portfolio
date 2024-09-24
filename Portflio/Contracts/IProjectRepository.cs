using System.Collections;

namespace Portflio.Contracts;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(int projectId);
    Task<IEnumerable<Project>> GetAllProjectsHostedOnAPlatform(int platformId);
    Task<IEnumerable<Project>> GetAllProjectsByType(int projectTypeId);
}