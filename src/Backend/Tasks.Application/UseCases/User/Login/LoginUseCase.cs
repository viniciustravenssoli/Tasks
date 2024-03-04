using Tasks.Application.Services.Cryptograpy;
using Tasks.Application.Services.Token;
using Tasks.Communication.Request;
using Tasks.Communication.Response;
using Tasks.Domain.Repositories.User;
using Tasks.Execptions.BaseExecptions;

namespace Tasks.Application.UseCases.User.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _usuarioReadOnlyRepository;
    private readonly PasswordEncryption _passwordEncryption;
    private readonly TokenController _tokenController;

    public LoginUseCase(IUserReadOnlyRepository usuarioReadOnlyRepository, PasswordEncryption passwordEncryption, TokenController tokenController)
    {
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        _passwordEncryption = passwordEncryption;
        _tokenController = tokenController;
    }

    public async Task<ResponseLogin> Execute(RequestLogin request)
    {
        var encryptedPassword = _passwordEncryption.Criptograph(request.Password);

        var user = await _usuarioReadOnlyRepository.GetByEmailAndPassword(request.Email, encryptedPassword);

        if (user == null)
        {
            throw new LoginInvalidException();
        }

        return new ResponseLogin
        {
            Name = user.Name,
            Token = _tokenController.GenerateToken(request.Email),
        };
    }
}
