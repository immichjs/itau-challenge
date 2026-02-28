using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class OrdemCompraConfiguration : IEntityTypeConfiguration<OrdemCompra>
{
    public void Configure(EntityTypeBuilder<OrdemCompra> builder)
    {
        builder.ToTable("OrdensCompra");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContaMasterId)
            .IsRequired();

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.PrecoUnitario)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.TipoMercado)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.DataExecucao)
            .IsRequired();

        builder.HasOne<ContaGrafica>()
            .WithMany()
            .HasForeignKey(x => x.ContaMasterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}