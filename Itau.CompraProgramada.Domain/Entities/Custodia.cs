namespace Itau.CompraProgramada.Domain.Entities;

public class Custodia
{
    public long Id { get; private set; }
    public long ContaGraficaId { get; private set; }
    public string Ticker { get; private set; } = null!;
    public int Quantidade { get; private set; }
    public decimal PrecoMedio { get; private set; }
    public DateTime DataUltimaAtualizacao { get; private set; }

    private Custodia() { }

    public Custodia(long contaGraficaId, string ticker)
    {
        if (contaGraficaId <= 0) throw new ArgumentOutOfRangeException(nameof(contaGraficaId));
        if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentException("Ticker é obrigatório.", nameof(ticker));

        ContaGraficaId = contaGraficaId;
        Ticker = ticker.Trim().ToUpperInvariant();

        Quantidade = 0;
        PrecoMedio = 0m;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void AplicarCompra(int quantidadeNova, decimal precoUnitario)
    {
        if (quantidadeNova <= 0) throw new ArgumentOutOfRangeException(nameof(quantidadeNova));
        if (precoUnitario <= 0) throw new ArgumentOutOfRangeException(nameof(precoUnitario));

        var qtdAnterior = Quantidade;
        var pmAnterior = PrecoMedio;

        var novoTotal = qtdAnterior + quantidadeNova;
        var novoPm = ((qtdAnterior * pmAnterior) + (quantidadeNova * precoUnitario)) / novoTotal;

        Quantidade = novoTotal;
        PrecoMedio = decimal.Round(novoPm, 8);
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void AplicarVenda(int quantidadeVendida)
    {
        if (quantidadeVendida <= 0) throw new ArgumentOutOfRangeException(nameof(quantidadeVendida));
        if (quantidadeVendida > Quantidade) throw new InvalidOperationException("Quantidade vendida maior que a posição.");

        Quantidade -= quantidadeVendida;
        DataUltimaAtualizacao = DateTime.UtcNow;

        if (Quantidade == 0)
            PrecoMedio = 0m;
    }
}