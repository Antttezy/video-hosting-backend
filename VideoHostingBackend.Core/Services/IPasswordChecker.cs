using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface IPasswordChecker
{
    public bool CheckPassword(UserCredentials credentials, string login, string password);
}