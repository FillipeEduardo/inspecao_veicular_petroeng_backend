using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class FotoConfiguration : IEntityTypeConfiguration<Foto>
{
    public void Configure(EntityTypeBuilder<Foto> builder)
    {
        builder.ToTable("fotos");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.NomeArquivo)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Extensao)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .HasOne(x => x.Evidencia)
            .WithMany(x => x.Fotos)
            .HasForeignKey(x => x.EvidenciaId);

        builder
            .HasOne(x => x.Vistoria)
            .WithMany(x => x.Fotos)
            .HasForeignKey(x => x.VistoriaId);
    }
}