using GerenciadorLivraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorLivraria.Infrastructure.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.HasData(
                new ProfileEntity
                {
                    Id = "0",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new ProfileEntity
                {
                    Id = "1",
                    Name = "Usuario",
                    NormalizedName = "USUARIO"
                }
            );
        }
    }
}