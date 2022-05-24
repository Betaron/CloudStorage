namespace CloudStorage.Data.Users;

public class UserDbModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}
