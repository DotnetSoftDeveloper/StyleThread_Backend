using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Context;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
        new Role { Id = 1, Name = "SuperAdministrator", NormalizedName = "SUPERADMINISTRATOR" },
        new Role { Id = 2, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
        new Role { Id = 3, Name = "Seller", NormalizedName = "SELLER" }, 
        new Role { Id = 4, Name = "Visitor", NormalizedName = "VISITOR" }
        );
    }
}
