using Itau.CompraProgramada.Application.Dtos.Cestas;
using Itau.CompraProgramada.Application.UseCases.Cestas;
using Microsoft.AspNetCore.Mvc;

namespace Itau.CompraProgramada.Api.Controllers;

[ApiController]
[Route("admin/cestas")]
public sealed class AdminCestasController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CestaResponse>> CreateOrUpdate(
        [FromBody] CreateOrUpdateCestaRequest request,
        [FromServices] CreateOrUpdateCestaTopFiveUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(request, ct);
        return Ok(result);
    }

    [HttpGet("atual")]
    public async Task<ActionResult<CestaResponse>> GetAtual(
        [FromServices] GetCestaAtualUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet("historico")]
    public async Task<ActionResult<CestaHistoricoResponse>> GetHistorico(
        [FromServices] GetHistoricoCestasUseCase useCase,
        CancellationToken ct)
    {
        var result = await useCase.ExecuteAsync(ct);
        return Ok(result);
    }
}