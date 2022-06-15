using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface IUserService
{
    Task<UserData> RegisterUser(string login, string password);
    Task<UserData?> LoginUser(string login, string password);

    Task<UserData?> GetById(int id);

    Task<UserData?> SetUserData(UserData newData);
}