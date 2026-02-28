using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class DistribuicaoConfiguration : IEntityTypeConfiguration<Distribuicao>
{
    public void Configure(EntityTypeBuilder<Distribuicao> builder)
    {
        builder.ToTable("Distribuicoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrdemCompraId).IsRequired();
        builder.Property(x => x.CustodiaFilhoteId).IsRequired();

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Quantidade).IsRequired();

        builder.Property(x => x.PrecoUnitario)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.DataDistribuicao)
            .IsRequired();

        builder.HasOne<OrdemCompra>()
            .WithMany()
            .HasForeignKey(x => x.OrdemCompraId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Custodia>()
            .WithMany()
            .HasForeignKey(x => x.CustodiaFilhoteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}