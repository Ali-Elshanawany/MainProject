
namespace FinalBoatSystemRental.Infrastructure.Seeding;

public class SeedingRoles : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> modelBuilder)
    {

        modelBuilder.HasData(new IdentityRole
        {
            Id = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a",
            Name = GlobalVariables.Admin,
            NormalizedName = GlobalVariables.NormalizedAdmin

        });

        modelBuilder.HasData(new IdentityRole
        {
            Id = "f117b498-2e53-4686-86dc-d3c13072850e",
            Name = GlobalVariables.Customer,
            NormalizedName = GlobalVariables.NormalizedCustomer,

        });

        modelBuilder.HasData(new IdentityRole
        {
            Id = "936c5f84-e463-49c2-bb6a-93347bbd5103",
            Name = GlobalVariables.Owner,
            NormalizedName = GlobalVariables.NormalizedOwner,

        });

    }
}
