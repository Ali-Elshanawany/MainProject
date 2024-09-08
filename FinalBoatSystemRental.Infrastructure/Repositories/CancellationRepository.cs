using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories
{
    public class CancellationRepository : BaseRepository<Cancellation>, ICancellationRepository
    {
        public CancellationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

    }
}
