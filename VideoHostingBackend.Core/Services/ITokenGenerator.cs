using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface ITokenGenerator
{
    Task<string> GenerateToken(UserCredentials credentials);
}