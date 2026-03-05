using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.ToTable("perfis");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasMaxLength(20);

        builder
            .HasIndex(x => x.Nome)
            .IsUnique();
    }
}