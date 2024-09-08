using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Configurations
{
    public class CancellationConfiguration : IEntityTypeConfiguration<Cancellation>
    {
        public void Configure(EntityTypeBuilder<Cancellation> builder)
        {
            throw new NotImplementedException();
        }
    }
}
