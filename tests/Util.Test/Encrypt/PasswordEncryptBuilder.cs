
using Tasks.Application.Services.Cryptograpy;

namespace Util.Test.Encrypt;
public class PasswordEncryptBuilder
{
    public static PasswordEncryption Instance()
    {
        return new PasswordEncryption("Abcd123");
    }
}
