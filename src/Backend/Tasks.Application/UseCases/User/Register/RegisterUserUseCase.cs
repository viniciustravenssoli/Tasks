using Tasks.Application.Services.Cryptograpy;
using Tasks.Application.Services.Token;
using Tasks.Communication.Request;
using Tasks.Communication.Response;
using Tasks.Domain.Repositories.User;
using Tasks.Execptions;
using Tasks.Execptions.BaseExecptions;
using Tasks.Infra;

namespace Tasks.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _usuarioReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncryption _passwordEncryption;
    private readonly TokenController _tokenController;

    public async Task<ResponseRegisterUser> Execute(RequestRegisterUser request)
    {
        await Validate(request);

        var entity = new Domain.Entities.User
        {
            Name = request.UserName,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Telefone,
        };

        entity.Password = _passwordEncryption.Criptograph(request.Password);

        await _usuarioRepository.Add(entity);
        await _unitOfWork.Commit();

        var token = _tokenController.GenerateToken(entity.Email);

        return new ResponseRegisterUser
        {
            Token = token,
        };
    }

    private async Task Validate(RequestRegisterUser request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var userExists = await _usuarioReadOnlyRepository.ExistUsuarioWithEmail(request.Email);

        if (userExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorsMessage.EmailAlreadyInUse));
        }

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationErrorException(errorsMessages);
        }
    }

}
