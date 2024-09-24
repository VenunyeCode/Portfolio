namespace Portflio.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _unitOfWork.Projects.GetAllProjectsAsync();
    }

    public async Task<Project> GetProjectById(int id)
    {
        return await _unitOfWork.Projects.GetProjectByIdAsync(id);
    }

    public async Task<IEnumerable<Project>> GetProjectsHostedOnAPlatform(int platformId)
    {
        return await _unitOfWork.Projects.GetAllProjectsHostedOnAPlatform(platformId);
    }

    public async Task<IEnumerable<Project>> GetProjectsByType(int projectTypeId)
    {
        return await _unitOfWork.Projects.GetAllProjectsByType(projectTypeId);
    }

    public async Task<Project> AddProject(Project project)
    {
        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.CommitAsync();
        
        return project;
    }

    public async Task UpdateProject(Project projectToBeUpdated, Project project)
    {
        projectToBeUpdated.Name = project.Name;
        projectToBeUpdated.Description = project.Description;
        projectToBeUpdated.StartDate = project.StartDate;
        projectToBeUpdated.EndDate = project.EndDate;
        projectToBeUpdated.ProjectTypeId = project.ProjectTypeId;
        
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteProject(Project project)
    {
        _unitOfWork.Projects.Remove(project);
        await _unitOfWork.CommitAsync();
    }
}