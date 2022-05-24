using CloudStorage.Core.Domains.Storages;
using CloudStorage.Core.Domains.Storages.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Data.Storages.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly CloudStorageContext _context;

    public StorageRepository(CloudStorageContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(
        Storage storage, CancellationToken cancellationToken)
    {
        var entity = new StorageDbModel()
        {
            Id = storage.Id,
            UserId = storage.UserId,
            Secret = storage.Secret
        };

        await _context.Storages.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(
        Storage storage, CancellationToken cancellationToken)
    {
        var entity = await _context.Storages
            .FirstOrDefaultAsync(it =>
                it.UserId == storage.UserId, cancellationToken);

        entity.Secret = storage.Secret;
    }

    public Task<bool> StorageExistsByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        return _context.Storages.AnyAsync(it =>
            it.Id == id, cancellationToken);
    }

    public async Task<Storage> GetByUserIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Storages.FirstOrDefaultAsync(it =>
            it.UserId == id, cancellationToken);

        return new Storage()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Secret = entity.Secret
        };
    }
}
