using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories
{
    public class ReservationAdditionRepository:BaseRepository<ReservationAddition>, IReservationAdditionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReservationAdditionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRange(List<ReservationAddition> list)
        {
            await _dbContext.ReservationAdditions.AddRangeAsync(list);
            _dbContext.SaveChanges();

        }

    }
}
