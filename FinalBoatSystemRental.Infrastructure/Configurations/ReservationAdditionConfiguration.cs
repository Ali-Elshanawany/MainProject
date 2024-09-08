using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations;

public class ReservationAdditionConfiguration : IEntityTypeConfiguration<ReservationAddition>
{
    public void Configure(EntityTypeBuilder<ReservationAddition> modelBuilder)
    {
        modelBuilder
           .HasOne(b => b.Reservation)
           .WithMany(a => a.ReservationAdditions)
           .HasForeignKey(b => b.ReservationId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}

