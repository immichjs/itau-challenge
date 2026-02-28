namespace Itau.CompraProgramada.Domain.Entities;

public class Distribuicao
{
    public long Id { get; private set; }
    public long OrdemCompraId { get; private set; }
    public long CustodiaFilhoteId { get; private set; }
    public string Ticker { get; private set; } = null!;
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public DateTime DataDistribuicao { get; private set; }

    private Distribuicao() { }

    public Distribuicao(long ordemCompraId, long custodiaFilhoteId, string ticker, int quantidade, decimal precoUnitario, DateTime dataDistribuicao)
    {
        if (ordemCompraId <= 0) throw new ArgumentOutOfRangeException(nameof(ordemCompraId));
        if (custodiaFilhoteId <= 0) throw new ArgumentOutOfRangeException(nameof(custodiaFilhoteId));
        if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentException("Ticker é obrigatório.", nameof(ticker));
        if (quantidade <= 0) throw new ArgumentOutOfRangeException(nameof(quantidade));
        if (precoUnitario <= 0) throw new ArgumentOutOfRangeException(nameof(precoUnitario));

        OrdemCompraId = ordemCompraId;
        CustodiaFilhoteId = custodiaFilhoteId;
        Ticker = ticker.Trim().ToUpperInvariant();
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        DataDistribuicao = dataDistribuicao;
    }
}