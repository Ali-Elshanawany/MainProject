using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder
                .HasOne(o => o.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
