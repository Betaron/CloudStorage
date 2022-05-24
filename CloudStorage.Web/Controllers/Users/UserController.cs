using CloudStorage.Core.Domains.Users;
using CloudStorage.Core.Domains.Users.Services;
using CloudStorage.Web.Controllers.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Web.Controllers.Users;

[ApiController]
[Route("user")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Adds a new user
    /// </summary>
    /// <param name="model">Template user</param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    public async Task<JsonResult> Create(
        AuthUserDto model, CancellationToken cancellationToken)
    {
        return new JsonResult(
            new
            {
                userId = await _userService.CreateAsync(new User
                {
                    Login = model.Login,
                    Password = model.Password
                }, cancellationToken)
            });
    }
}
