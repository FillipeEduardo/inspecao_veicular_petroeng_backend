using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("contratos");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasIndex(x => x.Nome)
            .IsUnique();
    }
}