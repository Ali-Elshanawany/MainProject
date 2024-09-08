using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories
{
    public class OwnerRepository:BaseRepository<Owner>, IOwnerRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public OwnerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<Owner> GetByUserId(string userId)
        {
           return await _dbcontext.Owners.FirstOrDefaultAsync(o=>o.User.Id == userId);
        } 
        public async Task<int> GetOwnerIdByUserId(string userId)
        {
           return await _dbcontext.Owners
                .AsNoTracking()
                .Where(i=>i.UserId == userId)
                .Select(a=>a.Id)
                .FirstOrDefaultAsync();
        }
    }
}
