using Itau.CompraProgramada.Domain.Enums;

namespace Itau.CompraProgramada.Domain.Entities;

public class OrdemCompra
{
    public long Id { get; private set; }
    public long ContaMasterId { get; private set; } 
    public string Ticker { get; private set; } = null!;

    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public ETipoMercado TipoMercado { get; private set; }
    public DateTime DataExecucao { get; private set; }

    private OrdemCompra() { }

    public OrdemCompra(long contaMasterId, string ticker, int quantidade, decimal precoUnitario, ETipoMercado tipoMercado, DateTime dataExecucao)
    {
        if (contaMasterId <= 0) throw new ArgumentOutOfRangeException(nameof(contaMasterId));
        if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentException("Ticker é obrigatório.", nameof(ticker));
        if (quantidade <= 0) throw new ArgumentOutOfRangeException(nameof(quantidade));
        if (precoUnitario <= 0) throw new ArgumentOutOfRangeException(nameof(precoUnitario));

        ContaMasterId = contaMasterId;
        Ticker = ticker.Trim().ToUpperInvariant();
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        TipoMercado = tipoMercado;
        DataExecucao = dataExecucao;
    }
}