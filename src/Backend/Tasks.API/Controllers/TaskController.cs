using Microsoft.AspNetCore.Mvc;
using Tasks.API.Filters;
using Tasks.Application.UseCases.Task.Delete;
using Tasks.Application.UseCases.Task.GetAllFromUser;
using Tasks.Application.UseCases.Task.Register;
using Tasks.Application.UseCases.Task.Update;
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

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar(
        [FromServices] IUpdateTaskUseCase useCase,
        [FromBody] RequestRegisterTask request,
        [FromRoute] long id)
    {
        await useCase.Execute(request, id);

        return NoContent();
    }

    [HttpGet]
    [Route("Pegar-Todas-Do-Usuario")]
    [ProducesResponseType(typeof(ResponseTaskAllFromUser), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarDashboard(
        [FromServices] IGetAllFromUser useCase,
        [FromQuery] RequestTasks request)
    {
        var result = await useCase.Execute(request);

        if (result.Tasks.Any())
        {
            return Ok(result);
        }

        return NoContent();
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar(
        [FromServices] IDeleteTaskUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Executar(id);

        return NoContent();
    }
}
