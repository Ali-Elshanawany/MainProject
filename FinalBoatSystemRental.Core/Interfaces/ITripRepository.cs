using FinalBoatSystemRental.Core.ViewModels.Trip;

namespace FinalBoatSystemRental.Core.Interfaces;

public interface ITripRepository : IBaseRepository<Trip>
{
    public Task<Trip> GetByIdAsync(int tripId, int ownerId);

    Task<IEnumerable<Trip>> GetAllAsync(int ownerid);

    // Return All Available Trips For User To Browse
    Task<IEnumerable<Trip>> GetAllAvailableTripsAsync();
    public Task<bool> GetConfirmedTripAsync(int boatId);

    public Task<bool> isTripAvailable(int boatId, DateTime startAt);

    public Task<List<AvailableTripsViewModel>> GetTripsWithAvailableSeats();

    public Task<GetCancellation_PriceViewModel> GetReservationTripPrice(int id);
    public Task<DateTime> GetReservationTripDeadline(int tripId);

    public Task<int> GetAvailableSeats(int tripId);


}
