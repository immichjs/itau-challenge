using Itau.CompraProgramada.Domain.Enums;

namespace Itau.CompraProgramada.Domain.Entities;

public class Rebalanceamento
{
    public long Id { get; private set; }

    public long ClienteId { get; private set; }
    public ETipoRebalanceamento Tipo { get; private set; }
    public string TickerVendido { get; private set; } = null!;
    public string TickerComprado { get; private set; } = null!;
    public decimal ValorVenda { get; private set; }
    public DateTime DataRebalanceamento { get; private set; }

    private Rebalanceamento() { }

    public Rebalanceamento(long clienteId, ETipoRebalanceamento tipo, string tickerVendido, string tickerComprado, decimal valorVenda, DateTime dataRebalanceamento)
    {
        if (clienteId <= 0) throw new ArgumentOutOfRangeException(nameof(clienteId));
        if (string.IsNullOrWhiteSpace(tickerVendido)) throw new ArgumentException("TickerVendido é obrigatório.", nameof(tickerVendido));
        if (string.IsNullOrWhiteSpace(tickerComprado)) throw new ArgumentException("TickerComprado é obrigatório.", nameof(tickerComprado));
        if (valorVenda < 0) throw new ArgumentOutOfRangeException(nameof(valorVenda));

        ClienteId = clienteId;
        Tipo = tipo;
        TickerVendido = tickerVendido.Trim().ToUpperInvariant();
        TickerComprado = tickerComprado.Trim().ToUpperInvariant();
        ValorVenda = valorVenda;
        DataRebalanceamento = dataRebalanceamento;
    }
}