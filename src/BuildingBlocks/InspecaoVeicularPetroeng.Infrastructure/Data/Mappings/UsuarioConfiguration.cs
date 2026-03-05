using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InspecaoVeicularPetroeng.Infrastructure.Data.Mappings;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Email)
            .HasMaxLength(50);

        builder
            .Property(x => x.Senha)
            .HasMaxLength(100);

        builder
            .Property(x => x.NomeCompleto)
            .HasMaxLength(100);

        builder
            .Property(x => x.Telefone)
            .HasMaxLength(11);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .HasOne(x => x.Perfil)
            .WithMany(x => x.Usuarios)
            .HasForeignKey(x => x.PerfilId);
    }
}