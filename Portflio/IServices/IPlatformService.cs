namespace Portflio.IServices;

public interface IPlatformService
{
    Task<IEnumerable<Platform>> GetAllPlatforms();
    Task<Platform> GetPlatformById(int id);
    Task<Platform> GetPlatformByProject(int projectId);
    Task<Platform> AddPlatform(Platform platform);
    Task UpdatePlatform(Platform platformToBeUpdated, Platform platform);
    Task DeletePlatform(Platform platform);
}