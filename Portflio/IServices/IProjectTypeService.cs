namespace Portflio.IServices;

public interface IProjectTypeService
{
    Task<IEnumerable<ProjectType>> GetAllProjectTypes();
    Task<ProjectType> GetProjectTypeById(int id);
    Task<ProjectType> AddProjectType(ProjectType projectType);
    Task UpdateProjectType(ProjectType projectTypeToBeUpdated, ProjectType projectType);
    Task DeleteProjectType(ProjectType projectType);
}