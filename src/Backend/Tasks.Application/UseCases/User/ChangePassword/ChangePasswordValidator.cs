using FluentValidation;
using Tasks.Communication.Request;

namespace Tasks.Application.UseCases.User.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(c => c.NewPassword).SetValidator(new PasswordValidator());
    }
}
