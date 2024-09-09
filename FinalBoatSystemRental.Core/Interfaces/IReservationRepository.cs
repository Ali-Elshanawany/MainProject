

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IReservationRepository : IBaseRepository<Reservation>
{

    Task<bool> isReservationFound(int tripId);


    public Task<IEnumerable<Reservation>> GetTripReservationCustomerHistory(int customerId);
    public Task<IEnumerable<Reservation>> GetTripReservationAdmin();
    public Task<IEnumerable<Reservation>> GetCanceledTripReservationAdmin();

    // Get ALl Owner Reservation With Status Active And Completed
    public Task<IEnumerable<Reservation>> GetOwnerReservation(int ownerId);

    // Get ALl Owner Reservation With Status Canceled
    public Task<IEnumerable<Reservation>> GetCanceledOwnerReservation(int ownerId);

    // For background job
    public Task<IEnumerable<Reservation>> GetPendingReservation();

}
