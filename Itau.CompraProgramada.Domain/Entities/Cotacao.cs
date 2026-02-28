namespace Itau.CompraProgramada.Domain.Entities;

public class Cotacao
{
    public long Id { get; private set; }
    public DateTime DataPregao { get; private set; }
    public string Ticker { get; private set; } = null!;
    public decimal PrecoAbertura { get; private set; }
    public decimal PrecoFechamento { get; private set; }
    public decimal PrecoMinimo { get; private set; }
    public decimal PrecoMaximo { get; private set; }

    private Cotacao() { }

    public Cotacao(DateTime dataPregao, string ticker, decimal abertura, decimal fechamento, decimal minimo, decimal maximo)
    {
        if (string.IsNullOrWhiteSpace(ticker)) throw new ArgumentException("Ticker é obrigatório.", nameof(ticker));
        if (abertura < 0 || fechamento < 0 || minimo < 0 || maximo < 0) throw new ArgumentOutOfRangeException("Preços não podem ser negativos.");

        DataPregao = dataPregao.Date;
        Ticker = ticker.Trim().ToUpperInvariant();
        PrecoAbertura = abertura;
        PrecoFechamento = fechamento;
        PrecoMinimo = minimo;
        PrecoMaximo = maximo;
    }
}