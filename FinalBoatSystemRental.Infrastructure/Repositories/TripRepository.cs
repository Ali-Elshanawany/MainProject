using FinalBoatSystemRental.Core.ViewModels.Trip;
using Microsoft.EntityFrameworkCore;

namespace FinalBoatSystemRental.Infrastructure.Repositories;


public class TripRepository:BaseRepository<Trip>, ITripRepository
{
    private readonly ApplicationDbContext _db;



    public TripRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    // Get Trip By id and Check That the Owner of the trip is the user Asking For Trip Details 
    public async new Task<Trip> GetByIdAsync(int tripId, int ownerId)
    {
        return await _db.Trips
                    .AsNoTracking()
                    .SingleOrDefaultAsync(a => a.Id==tripId && a.OwnerId==ownerId);
    }

    public async new Task<IEnumerable<Trip>> GetAllAsync(int ownerid)
    {
       return await _db.Trips
                        .AsNoTracking()
                        .Where(a=>a.OwnerId==ownerid)
                        .ToListAsync();
    }

    // Check if there is a Trip Reservation with Status (Active or Completed) and date  > the current Date 
    // If yes then return it and can not delete the boat in Delete Boat Function 

    public async Task<IEnumerable<Trip>> GetAllAvailableTripsAsync()
    {
        return await _db.Trips
                        .AsNoTracking()
                        .Include(b=>b.Boat)
                        .Where(a=>a.Status==GlobalVariables.TripActiveStatus)
                        .ToListAsync();
    }

    /* Check if There is a trip booked with the target id */
   
    public async Task<bool> GetConfirmedTripAsync(int boatId)
    {

        return await _db.Trips.Where(
            b => b.BoatId == boatId
            && (b.Status == GlobalVariables.TripActiveStatus || b.Status == GlobalVariables.TripCompletedStatus)
            && b.StartedAt >= DateTime.Now
            ).AnyAsync();
    }

    /*
     * Check if there is a trip on a boat at specific time 
     * This is used in Add Trip logic 
     * Owner can not Add 2 Trips on the same boat in the same day
     */

    public async Task<bool> isTripAvailable(int boatId, DateTime startAt)
    {
        return await _db.Trips
                    .AsNoTracking()
                    .AnyAsync(b => b.BoatId == boatId && b.StartedAt == startAt);
    }


    public async Task<List<AvailableTripsViewModel>> GetTripsWithAvailableSeats()
    {

        return await _db.Trips
                        .AsNoTracking()
                        .Select(t => new AvailableTripsViewModel
                        {
                            Name = t.Name,
                            Description = t.Description,
                            PricePerPerson = t.PricePerPerson,
                            MaxPeople = t.MaxPeople,
                            CancellationDeadLine = t.CancellationDeadLine,
                            Status = t.Status,
                            StartedAt = t.StartedAt,
                            BoatName = t.Boat.Name,
                            AvailableSeats = (t.MaxPeople- t.Reservations.Sum(b => b.NumOfPeople)),
                            CreatedAt = t.CreatedAt,
                            UpdatedAt = t.UpdatedAt,
                        })
                        .ToListAsync();
    }

    public async Task<GetCancellation_PriceViewModel> GetReservationTripPrice(int id)
    {
        return await _db.Trips
                                .AsNoTracking()
                                .Where(a => a.Id == id)
                                .Select(s=> new GetCancellation_PriceViewModel { Price=s.PricePerPerson,CancellationDeadLine=s.CancellationDeadLine ,ReservationDate=s.StartedAt })
                                .SingleOrDefaultAsync();
    }

    public async Task<DateTime> GetReservationTripDeadline(int tripId)
    {
        return await _db.Trips.AsNoTracking()
                                .Where(a=> a.Id == tripId)
                                .Select(b=>b.CancellationDeadLine)
                                .FirstOrDefaultAsync();
    }
    
}