using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class CotacaoConfiguration : IEntityTypeConfiguration<Cotacao>
{
    public void Configure(EntityTypeBuilder<Cotacao> builder)
    {
        builder.ToTable("Cotacoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataPregao)
            .IsRequired();

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.PrecoAbertura)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.PrecoFechamento)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.PrecoMaximo)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.PrecoMinimo)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.HasIndex(x => new { x.DataPregao, x.Ticker })
            .IsUnique();
    }
}