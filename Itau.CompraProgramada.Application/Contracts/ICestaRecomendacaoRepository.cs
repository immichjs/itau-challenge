using Itau.CompraProgramada.Application.Dtos.Cestas;

namespace Itau.CompraProgramada.Application.Contracts;

public interface ICestaRecomendacaoRepository
{
    Task<CestaResponse?> ObterCestaAtualAsync(CancellationToken ct);
    Task<List<CestaResponse>> ObterHistoricoAsync(CancellationToken ct);
    Task<CestaResponse> CriarOuAtualizarAsync(CreateOrUpdateCestaRequest request, CancellationToken ct);
}