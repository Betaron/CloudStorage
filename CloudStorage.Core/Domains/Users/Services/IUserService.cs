namespace CloudStorage.Core.Domains.Users.Services;

public interface IUserService
{
    /// <summary>
    /// Adds a new user to the repository. Copies an argument
    /// </summary>
    /// <param name="user">Template user</param>
    /// <param name="cancellationToken"></param>
    Task<Guid> CreateAsync(User user, CancellationToken cancellationToken);
}
