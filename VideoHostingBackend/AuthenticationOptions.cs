using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VideoHostingBackend;

public static class AuthenticationOptions
{
    private const string Key = "authentication_password";
    public static readonly TimeSpan Lifetime = TimeSpan.FromDays(7);

    public static SymmetricSecurityKey GetSecurityKey()
    {
        return new(Encoding.ASCII.GetBytes(Key));
    }
}