using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class StatusInspecaoConfiguration : IEntityTypeConfiguration<StatusInspecao>
{
    public void Configure(EntityTypeBuilder<StatusInspecao> builder)
    {
        builder.ToTable("status_inspecao");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .HasIndex(x => x.Nome)
            .IsUnique();
    }
}