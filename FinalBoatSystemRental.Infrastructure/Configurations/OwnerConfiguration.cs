using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> modelBuilder)
        {
            modelBuilder
                  .HasOne(o => o.User)
                  .WithOne(u => u.Owner)
                  .HasForeignKey<Owner>(o => o.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
