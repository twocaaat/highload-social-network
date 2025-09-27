using System.Security.Cryptography;
using System.Text;

namespace HighloadSocialNetwork.WebServer.Utils;

public static class PasswordHasher
{
    public static string MakeHash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool Verify(string password, string hash)
    {
        var passwordHash = MakeHash(password);
        return passwordHash == hash;
    }
}