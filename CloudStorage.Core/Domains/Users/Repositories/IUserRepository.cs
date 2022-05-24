namespace CloudStorage.Core.Domains.Users.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Adds a new user. Copies an argument
    /// <br/>
    /// <i>(You need to implement only the
    /// basic logic of working with the repository)</i>
    /// </summary>
    /// <param name="user">Template user</param>
    /// <param name="cancellationToken"></param>
    Task CreateAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Looking for a specific user by login
    /// </summary>
    /// <param name="login">User login</param>
    /// <returns>True if user exists</returns>
    /// <param name="cancellationToken"></param>
    Task<bool> UserExistsByLoginAsync(
        string login, CancellationToken cancellationToken);

    /// <summary>
    /// Looking for a specific user by Id
    /// </summary>
    /// <param name="id">User identification number</param>
    /// <returns>True if user exists</returns>
    Task<bool> UserExistsByIdAsync(
        Guid id, CancellationToken cancellationToken);
}
