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
}
