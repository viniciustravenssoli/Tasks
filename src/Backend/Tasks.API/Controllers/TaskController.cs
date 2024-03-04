using Microsoft.AspNetCore.Mvc;
using Tasks.API.Filters;
using Tasks.Application.UseCases.Task.Register;
using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.API.Controllers;
[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class TaskController : BaseTaskController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTask), StatusCodes.Status201Created)]
    public async Task<IActionResult> AdicionarTarefa(
        [FromServices] IRegisterTaskUseCase useCase,
        [FromBody] RequestRegisterTask request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
