using FinalBoatSystemRental.Core.ViewModels.BoatBooking;

namespace FinalBoatSystemRental.Infrastructure.Repositories;

public class BoatBookingRepository : BaseRepository<BoatBooking>, IBoatBookingRepository
{

    private readonly ApplicationDbContext _db;
    public BoatBookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public async Task<bool> IsBoatBookingFound(int boatId)
    {
        return await _db.BoatBookings
                        .Where(b => b.BoatId == boatId
                            && (b.Status == GlobalVariables.TripActiveStatus || b.Status == GlobalVariables.TripCompletedStatus)
                            && b.BookingDate >= DateTime.Now)
                            .AnyAsync();


    }

    /*
    * check if the date is available for the customer to book in 
    */
    public async Task<bool> CheckBoatAvialableDate(int boatId, DateTime date)
    {
        return await _db.BoatBookings
            .AsNoTracking()
            .AnyAsync(i => i.BoatId == boatId && i.BookingDate.Date == date.Date);

    }

    // Get History Boat Reservation For Customer 

    //public async Task<IEnumerable<BoatBooking>> GetBoatBookingCustomerHistory(int customerId)
    //{
    //    return await _db.BoatBookings.AsNoTracking()
    //                                 .Where(i=>i.CustomerId == customerId)
    //                                 .Where(a => a.Status == GlobalVariables.BoatBookingCanceledStatus || a.BookingDate.Date < DateTime.Now.Date)
    //                                 .ToListAsync();
    //}
    public async Task<IEnumerable<BoatBooking>> GetBoatBookingCustomerHistory(int customerId)
    {
        var now = DateTime.Now.Date; // Cache current date to avoid multiple calls
        return await _db.BoatBookings.AsNoTracking()
                                     .Where(i => i.CustomerId == customerId &&
                                                 (i.Status == GlobalVariables.BoatBookingCanceledStatus ||
                                                  i.BookingDate.Date < now)).OrderByDescending(i => i.BookingDate)
                                     .ToListAsync();
    }

    // return all boat booking reservation for admin to oversee 
    public async Task<IEnumerable<BoatBooking>> GetAllBoatBookingAdmin()
    {
        var now = DateTime.Now.Date; // Cache current date to avoid multiple calls
        return await _db.BoatBookings.AsNoTracking()
                                     .Where(i => i.Status != GlobalVariables.BoatBookingCanceledStatus)
                                     .Include(i => i.Boat)
                                     .OrderByDescending(i => i.BookingDate)
                                     .ToListAsync();
    }
    public async Task<IEnumerable<BoatBooking>> GetAllCanceledBoatBookingAdmin()
    {
        var now = DateTime.Now.Date; // Cache current date to avoid multiple calls
        return await _db.BoatBookings.AsNoTracking()
                                     .Where(i => i.Status == GlobalVariables.BoatBookingCanceledStatus)
                                     .Include(i => i.Boat)
                                     .OrderByDescending(i => i.BookingDate)
                                     .ToListAsync();
    }





    public async Task<IEnumerable<BoatBooking>> GetBoatBookingOwner(int ownerId)
    {
        return await _db.BoatBookings.AsNoTracking()
                                     .Include(i => i.Boat)
                                     .Where(i => i.Boat.OwnerId == ownerId &&
                                                 (i.Status != GlobalVariables.BoatBookingCanceledStatus)).OrderByDescending(a => a.BookingDate)
                                     .ToListAsync();
    }
    public async Task<IEnumerable<BoatBooking>> GetCanceledBoatBookingOwner(int ownerId)
    {
        return await _db.BoatBookings.AsNoTracking()
                                     .Include(i => i.Boat)
                                     .Where(i => i.Boat.OwnerId == ownerId &&
                                                 (i.Status == GlobalVariables.BoatBookingCanceledStatus)).OrderByDescending(a => a.BookingDate)
                                     .ToListAsync();
    }


}
