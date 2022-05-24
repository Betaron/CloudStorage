using CloudStorage.Core.Domains.Storages;
using CloudStorage.Core.Domains.Storages.Services;
using CloudStorage.Web.Controllers.Storages.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Web.Controllers.Storages;

[ApiController]
[Route("storage")]
public class StorageController
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    /// <summary>
    /// Set a secret by user id
    /// </summary>
    /// <param name="model">Template user</param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    public Task Create(
        SetStorageDto model, CancellationToken cancellationToken)
    {
        return _storageService.SetAsync(new Storage()
        {
            UserId = model.UserId,
            Secret = model.Secret
        }, cancellationToken);
    }

    /// <summary>
    /// Searches for a storage by user id
    /// </summary>
    /// <param name="id">User identification number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Found secret</returns>
    [HttpGet("{id}")]
    public async Task<JsonResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _storageService.GetByUserIdAsync(id, cancellationToken);

        return new JsonResult(
            new
            {
                secret = model.Secret
            });
    }
}
