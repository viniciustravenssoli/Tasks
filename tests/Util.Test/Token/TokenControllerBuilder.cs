using Tasks.Application.Services.Token;

namespace Util.Test.TokenController;
public class TokenControllerBuilder
{
    public static Tasks.Application.Services.Token.TokenController Instance()
    {
        return new Tasks.Application.Services.Token.TokenController(1000, "P8ejdhnwg03LmEBAx9N+WA==");
    }
}
