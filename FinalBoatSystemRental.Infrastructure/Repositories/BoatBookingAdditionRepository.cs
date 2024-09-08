using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories
{
    public class BoatBookingAdditionRepository:BaseRepository<BoatBookingAddition>, IBoatBookingAdditionRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public BoatBookingAdditionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {      
            _dbcontext = dbContext;
        }

        public async Task AddRange(List<BoatBookingAddition> list)
        {
            await _dbcontext.BoatBookingAdditions.AddRangeAsync(list);
            _dbcontext.SaveChanges();
            
        }
    }
}
