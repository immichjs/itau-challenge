using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class ItemCestaConfiguration : IEntityTypeConfiguration<ItemCesta>
{
    public void Configure(EntityTypeBuilder<ItemCesta> builder)
    {
        builder.ToTable("ItensCesta");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CestaId)
            .IsRequired();

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Percentual)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.HasIndex(x => new { x.CestaId, x.Ticker })
            .IsUnique();

        builder.HasOne<CestaRecomendacao>()
            .WithMany()
            .HasForeignKey(x => x.CestaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}