using Tasks.Application.Services.Cryptograpy;
using Tasks.Application.Services.LoggedUser;
using Tasks.Communication.Request;
using Tasks.Domain.Repositories.User;
using Tasks.Execptions;
using Tasks.Execptions.BaseExecptions;
using Tasks.Infra;

namespace Tasks.Application.UseCases.User.ChangePassword;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repositorio;
    private readonly PasswordEncryption _passwordEncryption;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repositorio, PasswordEncryption passwordEncryption, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repositorio = repositorio;
        _passwordEncryption = passwordEncryption;
        _unitOfWork = unitOfWork;
    }

    public async System.Threading.Tasks.Task Executar(RequestChangePassword request)
    {
        var loggedUser  = await _loggedUser.GetUser();

        var user = await _repositorio.GetById(loggedUser.Id);

        Validar(request, user);

        user.Password = _passwordEncryption.Criptograph(request.NewPassword);

        _repositorio.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validar(RequestChangePassword request, Domain.Entities.User user)
    {
        var validator = new ChangePasswordValidator();
        var resultado = validator.Validate(request);

        var senhaAtualCriptografada = _passwordEncryption.Criptograph(request.CurrentPassword);

        if (!user.Password.Equals(senhaAtualCriptografada))
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceErrorsMessage.CurrentPasswordInvalid));
        }

        if (!resultado.IsValid)
        {
            var mensagens = resultado.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(mensagens);
        }
    }
}
