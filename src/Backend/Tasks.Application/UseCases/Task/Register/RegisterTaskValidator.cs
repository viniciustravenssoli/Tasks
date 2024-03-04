using FluentValidation;
using Tasks.Communication.Request;
using Tasks.Execptions;

namespace Tasks.Application.UseCases.Task.Register;
public class RegisterTaskValidator : AbstractValidator<RequestRegisterTask>
{
    public RegisterTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyTaskName);
        RuleFor(x => x.Status).IsInEnum().WithMessage(ResourceErrorsMessage.InvalidStatus); ;
        RuleFor(x => x.Description).NotEmpty().WithMessage(ResourceErrorsMessage.EmptyDescription); 
    }
}
