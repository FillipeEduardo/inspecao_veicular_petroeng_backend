using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("veiculos");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Placa)
            .IsRequired()
            .HasMaxLength(7);

        builder
            .Property(x => x.Ano)
            .IsRequired();

        builder
            .Property(x => x.Modelo)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasIndex(x => x.Placa)
            .IsUnique();
    }
}