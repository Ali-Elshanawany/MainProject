using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class BoatBookingAdditionConfiguration : IEntityTypeConfiguration<BoatBookingAddition>
    {
        public void Configure(EntityTypeBuilder<BoatBookingAddition> modelBuilder)
        {
            modelBuilder
            .HasOne(b => b.BoatBooking)
            .WithMany(bb => bb.BoatBookingAdditions)
            .HasForeignKey(b => b.BoatBookingId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .HasOne(b => b.Addition)
                .WithMany(a => a.BoatBookingAdditions)
                .HasForeignKey(b => b.AdditionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
