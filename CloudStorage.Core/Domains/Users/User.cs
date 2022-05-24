namespace CloudStorage.Core.Domains.Users;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string Secret { get; set; }
}
