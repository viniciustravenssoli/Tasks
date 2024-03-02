using Microsoft.AspNetCore.Mvc;
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
}
