namespace Portflio.Repositories;

public class ProjectTypeRepository : Repository<ProjectType>, IProjectTypeRepository
{
    public PortfolioContext PortfolioContext => Context as PortfolioContext;
    public ProjectTypeRepository(DbContext context) : base(context) {}

    public async Task<ProjectType> GetProjectTypeById(int id)
    {
       return await PortfolioContext.ProjectTypes.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProjectType>> GetAllProjectTypes()
    {
       return await PortfolioContext.ProjectTypes.ToListAsync();
    }
}