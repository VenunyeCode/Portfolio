namespace Portflio.Services;

public class ProjectTypeService : IProjectTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ProjectType>> GetAllProjectTypes()
    {
        return await _unitOfWork.ProjectTypes.GetAllAsync();
    }

    public async Task<ProjectType> GetProjectTypeById(int id)
    {
        return await _unitOfWork.ProjectTypes.GetProjectTypeById(id);
    }

    public async Task<ProjectType> AddProjectType(ProjectType projectType)
    {
        await _unitOfWork.ProjectTypes.AddAsync(projectType);
        await _unitOfWork.CommitAsync();
        
        return projectType;
    }

    public async Task UpdateProjectType(ProjectType projectTypeToBeUpdated, ProjectType projectType)
    {
       projectTypeToBeUpdated.LabelType = projectType.LabelType;
       await _unitOfWork.CommitAsync();
    }

    public async Task DeleteProjectType(ProjectType projectType)
    {
       _unitOfWork.ProjectTypes.Remove(projectType);
       await _unitOfWork.CommitAsync();
    }
}