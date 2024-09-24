namespace Portflio.Contracts;

public interface IProjectTypeRepository : IRepository<ProjectType>
{
    Task<ProjectType> GetProjectTypeById(int id);
    Task<IEnumerable<ProjectType>> GetAllProjectTypes();
    
}