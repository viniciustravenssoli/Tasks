using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tasks.Application.Services.Token;
using Tasks.Communication.Response;
using Tasks.Execptions;
using Tasks.Domain.Repositories.User;

namespace Tasks.API.Filters;

public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _userRepository;

    public AuthenticatedUserAttribute(TokenController tokenController, IUserReadOnlyRepository userRepository)
    {
        _tokenController = tokenController;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = OnRequestToken(context);
            var email = _tokenController.GetEmail(token);

            var user = _userRepository.GetByEmail(email);

            if (user is null)
            {
                throw new Exception();
            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpired(context);
        }
        catch
        {
            UserWithoutPermission(context);
        }
    }

    private string OnRequestToken(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new Exception();
        }

        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpired(AuthorizationFilterContext filterContext)
    {
        filterContext.Result = new UnauthorizedObjectResult(new ErrorReponseJson(ResourceErrorsMessage.ExpiredToken));
    }

    private void UserWithoutPermission(AuthorizationFilterContext filterContext)
    {
        filterContext.Result = new UnauthorizedObjectResult(new ErrorReponseJson(ResourceErrorsMessage.NoPerm));
    }


}

