using AzureADB2C.Models;

namespace AzureADB2C.Services;

public interface IUserService
{
    Task<List<GetUserModel>> GetUsers();
    Task<GetUserModel> GetUserById(Guid id);
    Task<List<GetUserModel>> GetUserBySignInName(string signInName);
    Task CreateUser(List<CreateUserModel> userList);
    Task DeleteUserById(Guid id);
}
