namespace CloudStorage.Core.Domains.Users;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
