using CloudStorage.Core.Domains.Storages.Repositories;
using CloudStorage.Core.Domains.Users.Repositories;
using CloudStorage.Core.Exceptions;

namespace CloudStorage.Core.Domains.Storages.Services;

public class StorageService : IStorageService
{
    private readonly IStorageRepository _storageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StorageService(
        IStorageRepository storageRepository, 
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository)
    {
        _storageRepository = storageRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SetAsync(
        Storage storage, CancellationToken cancellationToken)
    {
        var userExists =
            await _userRepository.UserExistsByIdAsync(
                storage.UserId, cancellationToken);
        if (!userExists)
        {
            throw new ValidationException(validationMessage:
                $"Пользователя не существует.");
        }

        var storageExists =
            await _storageRepository.StorageExistsByIdAsync(
                storage.Id, cancellationToken);
        if (!storageExists)
        {
            storage.Id = new Guid();
            await _storageRepository.CreateAsync(storage, cancellationToken);
        }
        else
        {
            await _storageRepository.UpdateAsync(storage, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Storage> GetByUserIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        var userExists =
            await _userRepository.UserExistsByIdAsync(
                id, cancellationToken);
        if (!userExists)
        {
            throw new ValidationException(validationMessage:
                $"Пользователя не существует.");
        }

        return 
            await _storageRepository.GetByUserIdAsync(id, cancellationToken);
    }
}
