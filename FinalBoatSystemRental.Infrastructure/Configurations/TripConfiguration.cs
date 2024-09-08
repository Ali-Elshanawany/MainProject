using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> modelBuilder)
        {
           modelBuilder
          .HasOne(b => b.Owner)
          .WithMany(a => a.Trips)
          .HasForeignKey(b => b.OwnerId)
          .OnDelete(DeleteBehavior.NoAction);
        }
    }

   
}
