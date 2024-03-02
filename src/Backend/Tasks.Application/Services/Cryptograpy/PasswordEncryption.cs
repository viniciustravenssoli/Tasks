using System.Security.Cryptography;
using System.Text;

namespace Tasks.Application.Services.Cryptograpy;
public class PasswordEncryption
{
    private readonly string _key;
    public PasswordEncryption(string key)
    {
        _key = key;
    }

    public string Criptograph(string password)
    {
        var passwordWithKey = $"{password}{_key}";

        var bytes = Encoding.UTF8.GetBytes(passwordWithKey);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
