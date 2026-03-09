using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class StatusVistoriaConfiguration : IEntityTypeConfiguration<StatusVistoria>
{
    public void Configure(EntityTypeBuilder<StatusVistoria> builder)
    {
        builder.ToTable("status_vistoria");

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