namespace CloudStorage.Core;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
