using Itau.CompraProgramada.Application.Dtos.Cestas;

namespace Itau.CompraProgramada.Application.UseCases.Cestas;

internal static class CestaTopFiveValidator
{
    public static void Validar(CreateOrUpdateCestaRequest request)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome da cesta é obrigatório.", nameof(request.Nome));

        if (request.Itens is null || request.Itens.Count == 0)
            throw new ArgumentException("Itens da cesta são obrigatórios.", nameof(request.Itens));

        if (request.Itens.Count != 5)
            throw new InvalidOperationException("A cesta Top Five deve conter exatamente 5 ações.");

        foreach (var item in request.Itens)
        {
            if (string.IsNullOrWhiteSpace(item.Ticker))
                throw new ArgumentException("Ticker é obrigatório em todos os itens.");

            if (item.Percentual <= 0)
                throw new ArgumentException("Percentual deve ser maior que zero em todos os itens.");
        }

        var tickers = request.Itens
            .Select(i => i.Ticker.Trim().ToUpperInvariant())
            .ToList();

        if (tickers.Distinct().Count() != 5)
            throw new InvalidOperationException("A cesta não pode conter tickers duplicados.");

        var soma = request.Itens.Sum(i => i.Percentual);
        if (decimal.Round(soma, 2) != 100m)
            throw new InvalidOperationException("A soma dos percentuais da cesta deve ser 100%.");
    }
}