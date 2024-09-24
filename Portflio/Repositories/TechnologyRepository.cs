namespace Portflio.Repositories;

public class TechnologyRepository : Repository<Technology>, ITechnologyRepository
{
    public PortfolioContext PortfolioContext => Context as PortfolioContext;

    public TechnologyRepository(PortfolioContext context) : base(context) {}


    public async Task<IEnumerable<Technology>> GetAllTechnologiesAsync()
    {
        return await PortfolioContext.Technologies.ToListAsync();
    }

    public async Task<IEnumerable<Technology>> GetAllTechnologiesByProject(int projectId)
    {
        var project = await PortfolioContext.Projects.SingleOrDefaultAsync(x => x.Id == projectId);
        if(project == null)
            throw new KeyNotFoundException("Project not found");
        var technologies = await PortfolioContext.Technologies.Where(t => t.Id == project.Id).ToListAsync();
        //Logique à revoir;
        
        return technologies;
    }

    public async Task<Technology> GetTechnologyByIdAsync(int technologyId)
    {
        return await PortfolioContext.Technologies.SingleOrDefaultAsync(x => x.Id == technologyId);
    }
}