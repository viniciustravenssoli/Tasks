using Microsoft.AspNetCore.Mvc;
using Tasks.API.Filters;
using Tasks.Application.UseCases.User.ChangePassword;
using Tasks.Application.UseCases.User.Login;
using Tasks.Application.UseCases.User.Register;
using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.API.Controllers;
public class UsuarioController : BaseTaskController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUser), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUsuario(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUser request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [Route("Login")]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLogin), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] RequestLogin request,
                                           [FromServices] ILoginUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }

    [HttpPut]
    [Route("alterar-senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> AlterarSenha(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePassword request)
    {
        await useCase.Executar(request);

        return NoContent();
    }
}
