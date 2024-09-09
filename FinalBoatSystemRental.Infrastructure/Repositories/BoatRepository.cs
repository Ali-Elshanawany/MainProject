namespace FinalBoatSystemRental.Infrastructure.Repositories;



public class BoatRepository : BaseRepository<Boat>, IBoatRepository
{
    private readonly ApplicationDbContext _dbcontext;
    public BoatRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }

    public new async Task<Boat> GetByIdAsync(int id, int ownerId)
    {
        return await _dbcontext.Boats.FirstOrDefaultAsync(b => b.Id == id && b.OwnerId == ownerId);
    }

    public new async Task<IEnumerable<Boat>> GetAllAsync(int ownerId)
    {
        return await _dbcontext.Boats.Where(b => b.OwnerId == ownerId).OrderByDescending(a => a.CreatesAt).ToListAsync();
    }

    // Return All Approved Boats For Customer To Browse
    public async Task<IEnumerable<Boat>> GetAllAvailableBoatsAsync()
    {
        return await _dbcontext.Boats
                                .Where(b => b.Status == GlobalVariables.BoatApprovedStatus)
                                .ToListAsync();
    }
    public async Task<IEnumerable<Boat>> GetAllPendingBoatsAsync()
    {
        return await _dbcontext.Boats
                                .Where(b => b.Status == GlobalVariables.BoatPendingStatus)
                                .ToListAsync();
    }


    public async Task<bool> CheckBoatName(int id, string name)
    {
        return await _dbcontext.Boats.AnyAsync(o => o.OwnerId == id && o.Name == name);
    }


    // Return Reservation Price of the Desired Boat
    public async Task<decimal> GetReservationBoatPrice(int id)
    {
        return await _dbcontext.Boats
                                .AsNoTracking()
                                .Where(a => a.Id == id)
                                .Select(s => s.ReservationPrice)
                                .SingleOrDefaultAsync();
    }

    public async Task<int> GetMaxCancellationDateInDays(int boatId)
    {
        return await _dbcontext.Boats.AsNoTracking()
                                     .Where(a => a.Id == boatId)
                                     .Select(b => b.MaxCancellationDateInDays)
                                     .FirstOrDefaultAsync();
    }

    public async Task<bool> IsOwner(int boatId, int ownerid)
    {
        return await _dbcontext.Boats
                               .Where(a => a.OwnerId == ownerid && a.Id == boatId)
                               .AnyAsync();
    }

    // Check if there is a reservation on the boat at specific day


}
