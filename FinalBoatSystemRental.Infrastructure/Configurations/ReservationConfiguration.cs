using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> modelBuilder)
        {
        modelBuilder
         .HasOne(b => b.Trip)
         .WithMany(a => a.Reservations)
         .HasForeignKey(b => b.TripId)
         .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
