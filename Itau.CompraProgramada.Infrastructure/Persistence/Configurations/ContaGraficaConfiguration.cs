using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class ContaGraficaConfiguration : IEntityTypeConfiguration<ContaGrafica>
{
    public void Configure(EntityTypeBuilder<ContaGrafica> builder)
    {
        builder.ToTable("ContasGraficas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteId)
            .IsRequired(false);

        builder.Property(x => x.NumeroConta)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.NumeroConta).IsUnique();

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.HasOne<Cliente>()
            .WithOne()
            .HasForeignKey<ContaGrafica>(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}