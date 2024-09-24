using Portflio.Repositories;

namespace Portflio;

public class UnitOfWork : IUnitOfWork
{
    private readonly PortfolioContext _context;
    
    private IPlatformRepository _platformRepository;
    private IProjectRepository _projectRepository;
    private IProjectTypeRepository _projectTypeRepository;
    private ITechnologyRepository _technologyRepository;

    public UnitOfWork(PortfolioContext context)
    {
        _context = context;
    }


    public IPlatformRepository Platforms => _platformRepository = _platformRepository ?? new PlatformRepository(_context);

    public IProjectRepository Projects => _projectRepository = _projectRepository ?? new ProjectRepository(_context);

    public IProjectTypeRepository ProjectTypes => _projectTypeRepository = _projectTypeRepository ?? new ProjectTypeRepository(_context);

    public ITechnologyRepository Technologies => _technologyRepository = _technologyRepository ?? new TechnologyRepository(_context);

    public async Task<int> CommitAsync()
    {
       return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync(); 
    }

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackAsync(IDbContextTransaction transaction)
    {
        await transaction.RollbackAsync();
    }

    public async Task CreateSavePointAsync(IDbContextTransaction transaction, string savePointName)
    {
        await transaction.CreateSavepointAsync(savePointName);
    }

    public async Task RollBackSavePointAsync(IDbContextTransaction transaction, string savePointName)
    {
        await transaction.RollbackToSavepointAsync(savePointName);
    }

    public async Task ReleaseSavePointAsync(IDbContextTransaction transaction, string savePointName)
    {
       await transaction.ReleaseSavepointAsync(savePointName);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}