namespace Portflio.Contracts;

public interface IPlatformRepository : IRepository<Platform>
{
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();
    Task<Platform> GetPlatformByIdAsync(int id);
    Task<Platform> GetPlatformByProjectAsync(int projectId);
    
}