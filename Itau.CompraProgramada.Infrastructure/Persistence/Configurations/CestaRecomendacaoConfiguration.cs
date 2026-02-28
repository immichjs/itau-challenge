using Itau.CompraProgramada.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itau.CompraProgramada.Infrastructure.Persistence.Configurations;

public class CestaRecomendacaoConfiguration : IEntityTypeConfiguration<CestaRecomendacao>
{
    public void Configure(EntityTypeBuilder<CestaRecomendacao> builder)
    {
        builder.ToTable("CestasRecomendacao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Ativa)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Property(x => x.DataDesativacao)
            .IsRequired(false);
    }
}