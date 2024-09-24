namespace Portflio.IServices;

public interface ITechnologyService
{
    Task<IEnumerable<Technology>> GetAllTechnologies();
    Task<Technology> GetTechnologyById(int id);
    Task<IEnumerable<Technology>> GetTechnologiesUsedByProject(int projectId);
    Task<Technology> AddTechnology(Technology technology);
    Task UpdateTechnology(Technology technologyToBeUpdated, Technology technology);
    Task DeleteTechnology(Technology technology);
}