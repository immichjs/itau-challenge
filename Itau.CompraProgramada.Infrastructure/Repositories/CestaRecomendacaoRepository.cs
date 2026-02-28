using Itau.CompraProgramada.Api.Infrastructure;
using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Application.Dtos.Cestas;
using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Repositories;

public sealed class CestaRecomendacaoRepository : ICestaRecomendacaoRepository
{
    private readonly AppDbContext _db;

    public CestaRecomendacaoRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CestaResponse?> ObterCestaAtualAsync(CancellationToken ct)
    {
        var cesta = await _db.Set<CestaRecomendacao>()
            .AsNoTracking()
            .Where(x => x.Ativa)
            .OrderByDescending(x => x.DataCriacao)
            .FirstOrDefaultAsync(ct);

        if (cesta is null) return null;

        var itens = await _db.Set<ItemCesta>()
            .AsNoTracking()
            .Where(i => i.CestaId == cesta.Id)
            .OrderBy(i => i.Ticker)
            .ToListAsync(ct);

        return Map(cesta, itens);
    }

    public async Task<List<CestaResponse>> ObterHistoricoAsync(CancellationToken ct)
    {
        var cestas = await _db.Set<CestaRecomendacao>()
            .AsNoTracking()
            .OrderByDescending(x => x.DataCriacao)
            .ToListAsync(ct);

        if (cestas.Count == 0) return new List<CestaResponse>();

        var cestaIds = cestas.Select(c => c.Id).ToList();

        var itens = await _db.Set<ItemCesta>()
            .AsNoTracking()
            .Where(i => cestaIds.Contains(i.CestaId))
            .ToListAsync(ct);

        var itensPorCesta = itens
            .GroupBy(i => i.CestaId)
            .ToDictionary(g => g.Key, g => g.OrderBy(x => x.Ticker).ToList());

        return cestas.Select(c =>
        {
            itensPorCesta.TryGetValue(c.Id, out var itensDaCesta);
            itensDaCesta ??= new List<ItemCesta>();
            return Map(c, itensDaCesta);
        }).ToList();
    }

    public async Task<CestaResponse> CriarOuAtualizarAsync(CreateOrUpdateCestaRequest request, CancellationToken ct)
    {
        await using var trx = await _db.Database.BeginTransactionAsync(ct);

        var agora = DateTime.UtcNow;

        var cestaAtual = await _db.Set<CestaRecomendacao>()
            .Where(x => x.Ativa)
            .OrderByDescending(x => x.DataCriacao)
            .FirstOrDefaultAsync(ct);

        if (cestaAtual is not null)
        {
            var itensAtuais = await _db.Set<ItemCesta>()
                .AsNoTracking()
                .Where(i => i.CestaId == cestaAtual.Id)
                .ToListAsync(ct);

            if (MesmaComposicao(itensAtuais, request.Itens))
            {
                await trx.CommitAsync(ct);
                return Map(cestaAtual, itensAtuais);
            }

            cestaAtual.Desativar();
            _db.Update(cestaAtual);
            await _db.SaveChangesAsync(ct);
        }

        var novaCesta = new CestaRecomendacao(request.Nome);

        _db.Add(novaCesta);
        await _db.SaveChangesAsync(ct);

        var itensNovos = request.Itens.Select(i =>
            new ItemCesta(
                cestaId: novaCesta.Id,
                ticker: i.Ticker,
                percentual: i.Percentual
            )
        ).ToList();

        _db.AddRange(itensNovos);
        await _db.SaveChangesAsync(ct);

        await trx.CommitAsync(ct);

        return Map(novaCesta, itensNovos);
    }

    private static bool MesmaComposicao(List<ItemCesta> itensAtuais, List<CestaItemRequest> itensRequest)
    {
        if (itensAtuais.Count != itensRequest.Count) return false;

        var a = itensAtuais
            .Select(x => (Ticker: x.Ticker.Trim().ToUpperInvariant(), Percentual: decimal.Round(x.Percentual, 2)))
            .OrderBy(x => x.Ticker)
            .ToList();

        var b = itensRequest
            .Select(x => (Ticker: x.Ticker.Trim().ToUpperInvariant(), Percentual: decimal.Round(x.Percentual, 2)))
            .OrderBy(x => x.Ticker)
            .ToList();

        return a.SequenceEqual(b);
    }

    private static CestaResponse Map(CestaRecomendacao cesta, List<ItemCesta> itens)
        => new(
            Id: cesta.Id,
            Nome: cesta.Nome,
            Ativa: cesta.Ativa,
            DataCriacao: cesta.DataCriacao,
            DataDesativacao: cesta.DataDesativacao,
            Itens: itens.Select(i => new CestaItemResponse(i.Ticker, i.Percentual)).ToList()
        );
}