using System.Security.Cryptography;
using System.Text;

namespace VideoHostingBackend.Util;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using SHA384 sha = SHA384.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(sha.ComputeHash(bytes));
    }
}