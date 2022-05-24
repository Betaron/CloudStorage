using CloudStorage.Core.Domains.Users.Repositories;

namespace CloudStorage.Core.Domains.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(
        User user, CancellationToken cancellationToken)
    {
        var loginMaxLength = 20;
        if (user.Login.Length > loginMaxLength)
        {
            throw new Exception(message:
                $"Длина логина не должна превышать {loginMaxLength}");
        }

        var userExists =
           await _userRepository.UserExistsByLoginAsync(
                user.Login, cancellationToken);
        if (userExists)
        {
            throw new Exception(message: 
                $"Пользователь с логином {user.Login} существует.");
        }

        user.Id = new Guid();

        await _userRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
    }
}
