namespace Portflio.Services;

public class TechnologyService : ITechnologyService
{
    private readonly IUnitOfWork _unitOfWork;

    public TechnologyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Technology>> GetAllTechnologies()
    {
        return await _unitOfWork.Technologies.GetAllAsync();
    }

    public async Task<Technology> GetTechnologyById(int id)
    {
        return await _unitOfWork.Technologies.GetTechnologyByIdAsync(id);
    }

    public async Task<IEnumerable<Technology>> GetTechnologiesUsedByProject(int projectId)
    {
        return await _unitOfWork.Technologies.GetAllTechnologiesByProject(projectId);
    }

    public async Task<Technology> AddTechnology(Technology technology)
    {
        await _unitOfWork.Technologies.AddAsync(technology);
        await _unitOfWork.CommitAsync();
        
        return technology;
    }

    public async Task UpdateTechnology(Technology technologyToBeUpdated, Technology technology)
    {
        technologyToBeUpdated.TechnoLabel = technologyToBeUpdated.TechnoLabel;
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteTechnology(Technology technology)
    {
        _unitOfWork.Technologies.Remove(technology);
        await _unitOfWork.CommitAsync();
    }
}