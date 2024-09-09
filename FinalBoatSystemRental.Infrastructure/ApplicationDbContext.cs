
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FinalBoatSystemRental.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Addition> Additions { get; set; }
    public DbSet<Boat> Boats { get; set; }
    public DbSet<BoatBooking> BoatBookings { get; set; }
    public DbSet<BoatBookingAddition> BoatBookingAdditions { get; set; }
    public DbSet<Cancellation> Cancellations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationAddition> ReservationAdditions { get; set; }
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BoatBookingAdditionConfiguration());
        modelBuilder.ApplyConfiguration(new TripConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationAdditionConfiguration());
        modelBuilder.ApplyConfiguration(new OwnerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new SeedingRoles());


        //#region Seeding
        //var hasher = new PasswordHasher<ApplicationUser>();

        //modelBuilder.Entity<ApplicationUser>().HasData(
        //    new ApplicationUser
        //    {
        //        Id = Guid.NewGuid().ToString(), // Primary Key
        //        UserName = "Customer1",
        //        NormalizedUserName = "CUSTOMER1",
        //        Email = "Customer1@example.com",
        //        NormalizedEmail = "CUSTOMER1@EXAMPLE.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "Password123!*"),
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    },
        //    new ApplicationUser
        //    {
        //        Id = Guid.NewGuid().ToString(), // Primary Key
        //        UserName = "user2",
        //        NormalizedUserName = "USER2",
        //        Email = "user2@example.com",
        //        NormalizedEmail = "USER2@EXAMPLE.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "Password123!"),
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    }
        //);

        //#endregion


    }
}



