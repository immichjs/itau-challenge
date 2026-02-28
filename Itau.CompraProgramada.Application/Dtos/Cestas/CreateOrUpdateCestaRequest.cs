namespace Itau.CompraProgramada.Application.Dtos.Cestas;

public sealed record CreateOrUpdateCestaRequest(
    string Nome,
    List<CestaItemRequest> Itens
);