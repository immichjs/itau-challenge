using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class CustodiaConfiguration : IEntityTypeConfiguration<Custodia>
{
    public void Configure(EntityTypeBuilder<Custodia> builder)
    {
        builder.ToTable("Custodias");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContaGraficaId)
            .IsRequired();

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.PrecoMedio)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.DataUltimaAtualizacao)
            .IsRequired();

        builder.HasIndex(x => new { x.ContaGraficaId, x.Ticker })
            .IsUnique();

        builder.HasOne<ContaGrafica>()
            .WithMany()
            .HasForeignKey(x => x.ContaGraficaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}