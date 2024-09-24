namespace Portflio.Contracts;

public interface IUnitOfWork : IDisposable
{
    IPlatformRepository Platforms { get; }
    IProjectRepository Projects { get; }
    IProjectTypeRepository ProjectTypes { get; }
    ITechnologyRepository Technologies { get; }
    
    Task<int> CommitAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync(IDbContextTransaction transaction);
    Task RollbackAsync(IDbContextTransaction transaction);
    Task CreateSavePointAsync(IDbContextTransaction transaction, string savePointName);
    Task RollBackSavePointAsync(IDbContextTransaction transaction, string savePointName);
    Task ReleaseSavePointAsync(IDbContextTransaction transaction, string savePointName);
    IExecutionStrategy CreateExecutionStrategy();
    
    
}