using FluentValidation;
using System.Text.RegularExpressions;
using Tasks.Communication.Request;
using Tasks.Execptions;

namespace Tasks.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.UserName).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyUserName);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyUserEmail);
        RuleFor(c => c.Telefone).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyPhone);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErrorsMessage.InvalidUserEmail);
        });

        When(c => !string.IsNullOrEmpty(c.Telefone), () =>
        {
            RuleFor(c => c.Telefone).Custom((telefone, context) =>
            {
                string patternPhone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telefone, patternPhone);

                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceErrorsMessage.InvalidPhone));
                }
            });
        });

    }
}
