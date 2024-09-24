namespace Portflio.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public PortfolioContext PortfolioContext => Context as PortfolioContext;
    public ProjectRepository(DbContext context) : base(context) {}

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await PortfolioContext.Projects.ToListAsync();
    }

    public Task<Project> GetProjectByIdAsync(int projectId)
    {
        var doesProjectExist = PortfolioContext.Projects.Any(p => p.Id == projectId);
        if(doesProjectExist)
            return PortfolioContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        else
        {
            return Task.FromResult<Project>(null);
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjectsHostedOnAPlatform(int platformId)
    {
        var platform = PortfolioContext.Platforms.FirstOrDefault(p => p.Id == platformId);
        if(platform == null) throw new Exception($"Platform {platformId} not found");
        
        return await PortfolioContext.Projects.Where(p => p.PlatformId == platform.Id).ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetAllProjectsByType(int projectTypeId)
    {
       var projectType = PortfolioContext.ProjectTypes.FirstOrDefault(t => t.Id == projectTypeId);
       if(projectType == null) throw new Exception($"ProjectType {projectTypeId} not found");
       
       return await PortfolioContext.Projects.Where(p => p.ProjectTypeId == projectType.Id).ToListAsync();
    }
}