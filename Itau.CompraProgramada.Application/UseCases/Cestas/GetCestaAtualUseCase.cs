using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Application.Dtos.Cestas;

namespace Itau.CompraProgramada.Application.UseCases.Cestas;

public sealed class GetCestaAtualUseCase
{
    private readonly ICestaRecomendacaoRepository _repo;

    public GetCestaAtualUseCase(ICestaRecomendacaoRepository repo)
    {
        _repo = repo;
    }

    public Task<CestaResponse?> ExecuteAsync(CancellationToken ct = default)
        => _repo.ObterCestaAtualAsync(ct);
}