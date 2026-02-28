using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Application.Dtos.Cestas;

namespace Itau.CompraProgramada.Application.UseCases.Cestas;

public sealed class GetHistoricoCestasUseCase
{
    private readonly ICestaRecomendacaoRepository _repo;

    public GetHistoricoCestasUseCase(ICestaRecomendacaoRepository repo)
    {
        _repo = repo;
    }

    public async Task<CestaHistoricoResponse> ExecuteAsync(CancellationToken ct = default)
    {
        var cestas = await _repo.ObterHistoricoAsync(ct);
        return new CestaHistoricoResponse(cestas);
    }
}