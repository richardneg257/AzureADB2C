using AzureADB2C.Models;
using AzureADB2C.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureADB2C.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<GetUserModel>> Get()
    {
        return await _userService.GetUsers();
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    public async Task<GetUserModel> GetUserById([FromRoute] Guid id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpGet("search")]
    public async Task<List<GetUserModel>> GetUserBySignInName([FromQuery] string signInName)
    {
        return await _userService.GetUserBySignInName(signInName);
    }

    [HttpPost]
    public async Task CreateUser([FromBody] List<CreateUserModel> userList)
    {
        await _userService.CreateUser(userList);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteUserById(Guid id)
    {
        await _userService.DeleteUserById(id);
    }
}
