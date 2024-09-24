namespace Portflio.Contracts;

public interface ITechnologyRepository : IRepository<Technology>
{
    Task<IEnumerable<Technology>> GetAllTechnologiesAsync();
    Task<IEnumerable<Technology>> GetAllTechnologiesByProject(int projectId);
    Task<Technology> GetTechnologyByIdAsync(int technologyId);
}