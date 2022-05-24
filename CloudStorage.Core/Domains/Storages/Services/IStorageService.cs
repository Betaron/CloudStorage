namespace CloudStorage.Core.Domains.Storages.Services;

public interface IStorageService
{
    /// <summary>
    /// Adds a new storage to the repository. Copies an argument
    /// </summary>
    /// <param name="storage">Template storage</param>
    /// <param name="cancellationToken"></param>
    Task SetAsync(
        Storage storage, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for a storage by user id
    /// </summary>
    /// <param name="id">User identification number</param>
    /// <returns>Found user</returns>
    /// <param name="cancellationToken"></param>
    Task<Storage> GetByUserIdAsync(
        Guid id, CancellationToken cancellationToken);
}
