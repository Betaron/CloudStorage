using CloudStorage.Core;

namespace CloudStorage.Data;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CloudStorageContext _context;

    public UnitOfWork(CloudStorageContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
