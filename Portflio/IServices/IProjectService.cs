namespace Portflio.IServices;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Project> GetProjectById(int id);
    Task<IEnumerable<Project>> GetProjectsHostedOnAPlatform(int platformId);
    Task<IEnumerable<Project>> GetProjectsByType(int projectTypeId);
    Task<Project> AddProject(Project project);
    Task UpdateProject(Project projectToBeUpdated, Project project);
    Task DeleteProject(Project project);
}