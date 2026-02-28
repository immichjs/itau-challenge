namespace Itau.CompraProgramada.Domain.Entities;

public class CestaRecomendacao
{
    public long Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public bool Ativa { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataDesativacao { get; private set; }
    public List<ItemCesta> Itens { get; private set; } = new();

    private CestaRecomendacao() { }

    public CestaRecomendacao(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));

        Nome = nome.Trim();
        Ativa = true;
        DataCriacao = DateTime.UtcNow;
        DataDesativacao = null;
    }

    public void Desativar()
    {
        Ativa = false;
        DataDesativacao = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Ativa = true;
        DataDesativacao = null;
    }

    public void DefinirItens(List<ItemCesta> itens)
    {
        if (itens is null) throw new ArgumentNullException(nameof(itens));
        if (itens.Count != 5) throw new InvalidOperationException("A cesta Top Five deve conter exatamente 5 ações.");

        var soma = itens.Sum(i => i.Percentual);
        if (decimal.Round(soma, 2) != 100m)
            throw new InvalidOperationException("A soma dos percentuais da cesta deve ser 100%.");

        // tickers únicos
        var duplicado = itens
            .GroupBy(i => i.Ticker.Trim().ToUpperInvariant())
            .Any(g => g.Count() > 1);
        if (duplicado) throw new InvalidOperationException("A cesta não pode conter tickers duplicados.");

        Itens = itens;
    }
}