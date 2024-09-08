

using FinalBoatSystemRental.Core.ViewModels.BoatBooking;

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IBoatBookingRepository : IBaseRepository<BoatBooking>
{

    public Task<bool> IsBoatBookingFound(int boatId);

    public Task<bool> CheckBoatAvialableDate(int boatId, DateTime date);

    // Get History Boat Reservation For Customer 
    public Task<IEnumerable<BoatBooking>> GetBoatBookingCustomerHistory(int customerId);

    // Get ALl Owner Reservation With Status Active And Completed
    public Task<IEnumerable<BoatBooking>> GetBoatBookingOwner(int ownerId);

    // Get ALl Owner Reservation With Status Canceled
    public Task<IEnumerable<BoatBooking>> GetCanceledBoatBookingOwner(int ownerId);





}
