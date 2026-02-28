using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class EventoIrConfiguration : IEntityTypeConfiguration<EventoIr>
{
    public void Configure(EntityTypeBuilder<EventoIr> builder)
    {
        builder.ToTable("EventosIR");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteId)
            .IsRequired();

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.ValorBase)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ValorIr)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PublicadoKafka)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.DataEvento)
            .IsRequired();

        builder.HasOne<Cliente>()
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}