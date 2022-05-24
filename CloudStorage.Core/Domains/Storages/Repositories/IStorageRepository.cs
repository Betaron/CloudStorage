namespace CloudStorage.Core.Domains.Storages.Repositories;

public interface IStorageRepository
{
    /// <summary>
    /// Adds a new storage to the repository. Copies an argument
    /// <br/>
    /// <i>(You need to implement only the
    /// basic logic of working with the repository)</i>
    /// </summary>
    /// <param name="storage">Template storage</param>
    /// <param name="cancellationToken"></param>
    Task CreateAsync(
        Storage storage, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the storage in the repository. Copies an argument
    /// <br/>
    /// <i>(You need to implement only the
    /// basic logic of working with the repository)</i>
    /// </summary>
    /// <param name="storage">Template storage</param>
    /// <param name="cancellationToken"></param>
    Task UpdateAsync(
        Storage storage, CancellationToken cancellationToken);

    /// <summary>
    /// Looking for a specific storage by Id
    /// </summary>
    /// <param name="id">Storage identification number</param>
    /// <returns>True if user exists</returns>
    Task<bool> StorageExistsByIdAsync(
        Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for a storage by user id
    /// <br/>
    /// <i>(You need to implement only the
    /// basic logic of working with the repository)</i>
    /// </summary>
    /// <param name="id">User identification number</param>
    /// <returns>Found user</returns>
    /// <param name="cancellationToken"></param>
    Task<Storage> GetByUserIdAsync(
        Guid id, CancellationToken cancellationToken);
}
