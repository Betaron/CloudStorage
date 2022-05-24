using System.Security.Cryptography;
using CloudStorage.Core.Domains.Users;
using CloudStorage.Core.Domains.Users.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Data.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CloudStorageContext _context;

    public UserRepository(CloudStorageContext context)
    {
        _context = context;
    }

    private (string hash, byte[] salt) MakeHash(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        Console.WriteLine($"Hashed: {hashed}");

        return (hashed, salt);
    }

    public async Task CreateAsync(
        User user, CancellationToken cancellationToken)
    {
        var passwordHash = MakeHash(user.Password);

        var entity = new UserDbModel()
        {
            Id = user.Id,
            Login = user.Login,
            PasswordHash = passwordHash.hash,
            Salt = passwordHash.salt
        };

        await _context.Users.AddAsync(entity, cancellationToken);
    }

    public Task<bool> UserExistsByLoginAsync(
        string login, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(it =>
            it.Login == login, cancellationToken);
    }
}
