using FluentValidation;
using Tasks.Execptions;

namespace Tasks.Application.UseCases.User;
public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(p => p).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyPassword);
        When(p => !string.IsNullOrEmpty(p), () =>
        {
            RuleFor(p => p.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorsMessage.PasswordMust6);
        });
    }
}