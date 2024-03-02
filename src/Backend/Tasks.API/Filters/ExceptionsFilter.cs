using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Tasks.Execptions.BaseExecptions;
using Tasks.Execptions;
using Tasks.Communication.Response;


namespace Tasks.API.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is TaskExecptions)
        {
            DealLivroReceitasExceptions(context);
        }
        else
        {
            ThrowUnkownError(context);
        }
    }

    private void DealLivroReceitasExceptions(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorException)
        {
            DealValidationErrorsException(context);
        }
        else if (context.Exception is LoginInvalidException)
        {
            DealLoginException(context);
        }
    }

    private void DealValidationErrorsException(ExceptionContext context)
    {
        var validationErrorException = context.Exception as ValidationErrorException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ErrorReponseJson(validationErrorException.ErrorMessages));
    }
    private void DealLoginException(ExceptionContext context)
    {
        var erroLogin = context.Exception as LoginInvalidException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ErrorReponseJson(erroLogin.Message));
    }

    private void ThrowUnkownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorReponseJson(ResourceErrorsMessage.UnknowError));
    }
}
