using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class VistoriaConfiguration : IEntityTypeConfiguration<Vistoria>
{
    public void Configure(EntityTypeBuilder<Vistoria> builder)
    {
        builder.ToTable("vistorias");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Data)
            .IsRequired();

        builder
            .Property(x => x.QuilometragemVeiculo)
            .IsRequired();

        builder
            .Property(x => x.Observacao);

        builder
            .HasOne(x => x.Status)
            .WithMany(x => x.Vistorias)
            .HasForeignKey(x => x.StatusId);

        builder
            .HasOne(x => x.Veiculo)
            .WithMany(x => x.Vistorias)
            .HasForeignKey(x => x.VeiculoId);
    }
}