using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class RebalanceamentoConfiguration : IEntityTypeConfiguration<Rebalanceamento>
{
    public void Configure(EntityTypeBuilder<Rebalanceamento> builder)
    {
        builder.ToTable("Rebalanceamentos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteId).IsRequired();

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.TickerVendido)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.TickerComprado)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.ValorVenda)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DataRebalanceamento)
            .IsRequired();

        builder.HasOne<Cliente>()
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}