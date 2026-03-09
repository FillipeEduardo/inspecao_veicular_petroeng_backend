using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class InspecaoConfiguration : IEntityTypeConfiguration<Inspecao>
{
    public void Configure(EntityTypeBuilder<Inspecao> builder)
    {
        builder.ToTable("inspecoes");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .HasOne(x => x.Vistoria)
            .WithMany(x => x.Inspecoes)
            .HasForeignKey(x => x.VistoriaId);

        builder
            .HasOne(x => x.Item)
            .WithMany(x => x.Inspecoes)
            .HasForeignKey(x => x.ItemId);
    }
}