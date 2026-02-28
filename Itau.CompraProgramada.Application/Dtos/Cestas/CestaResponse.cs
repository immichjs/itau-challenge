namespace Itau.CompraProgramada.Application.Dtos.Cestas;

public sealed record CestaResponse(
    long Id,
    string Nome,
    bool Ativa,
    DateTime DataCriacao,
    DateTime? DataDesativacao,
    List<CestaItemResponse> Itens
);