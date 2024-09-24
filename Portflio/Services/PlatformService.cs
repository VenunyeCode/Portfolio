namespace Portflio.Services;

public class PlatformService : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork;

    public PlatformService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Platform>> GetAllPlatforms()
    {
        return await _unitOfWork.Platforms.GetAllAsync();
    }

    public async Task<Platform> GetPlatformById(int id)
    {
        return await _unitOfWork.Platforms.GetPlatformByIdAsync(id);
    }

    public async Task<Platform> GetPlatformByProject(int projectId)
    {
        return await _unitOfWork.Platforms.GetPlatformByProjectAsync(projectId);
    }

    public async Task<Platform> AddPlatform(Platform platform)
    {
       await _unitOfWork.Platforms.AddAsync(platform);
       await _unitOfWork.CommitAsync();
       
       return platform;
    }

    public async Task UpdatePlatform(Platform platformToBeUpdated, Platform platform)
    {
       platformToBeUpdated.PlatFormLabel = platform.PlatFormLabel;
       await _unitOfWork.CommitAsync();
    }

    public async Task DeletePlatform(Platform platform)
    {
         _unitOfWork.Platforms.Remove(platform);
         await _unitOfWork.CommitAsync();
    }
}