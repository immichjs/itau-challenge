namespace Itau.CompraProgramada.Domain.Entities;

public class ItemCesta
{
    public long Id { get; private set; }
    public long CestaId { get; private set; }
    public string Ticker { get; private set; } = null!;
    public decimal Percentual { get; private set; }

    private ItemCesta() { }

    public ItemCesta(long cestaId, string ticker, decimal percentual)
    {
        if (cestaId <= 0) throw new ArgumentOutOfRangeException(nameof(cestaId));
        if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentException("Ticker é obrigatório.", nameof(ticker));
        if (percentual <= 0) throw new ArgumentOutOfRangeException(nameof(percentual));

        CestaId = cestaId;
        Ticker = ticker.Trim().ToUpperInvariant();
        Percentual = percentual;
    }
}