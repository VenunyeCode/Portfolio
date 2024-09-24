namespace Portflio.Repositories;

public class PlatformRepository : Repository<Platform>, IPlatformRepository
{
    public PortfolioContext PortfolioContext => Context as PortfolioContext;
    public PlatformRepository(PortfolioContext context) : base(context) {}

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        return await PortfolioContext.Platforms.ToListAsync();
    }

    public async Task<Platform> GetPlatformByIdAsync(int id)
    {
        return await PortfolioContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Platform> GetPlatformByProjectAsync(int projectId)
    {
        var project = PortfolioContext.Projects.FirstOrDefault(p => p.Id == projectId);
        if(project == null) throw new Exception($"Project with id {projectId} not found");
        return PortfolioContext.Platforms.FirstOrDefaultAsync(p => p.Id == project.PlatformId);
    }
}