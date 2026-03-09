using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class EvidenciaConfiguration : IEntityTypeConfiguration<Evidencia>
{
    public void Configure(EntityTypeBuilder<Evidencia> builder)
    {
        builder.ToTable("evidencias");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasIndex(x => x.Nome)
            .IsUnique();
    }
}