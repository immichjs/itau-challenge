using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Application.Dtos.Cestas;

namespace Itau.CompraProgramada.Application.UseCases.Cestas;

public sealed class CreateOrUpdateCestaTopFiveUseCase
{
    private readonly ICestaRecomendacaoRepository _repo;

    public CreateOrUpdateCestaTopFiveUseCase(ICestaRecomendacaoRepository repo)
    {
        _repo = repo;
    }

    public async Task<CestaResponse> ExecuteAsync(CreateOrUpdateCestaRequest request, CancellationToken ct = default)
    {
        CestaTopFiveValidator.Validar(request);
        return await _repo.CriarOuAtualizarAsync(request, ct);
    }
}