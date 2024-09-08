

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IReservationAdditionRepository : IBaseRepository<ReservationAddition>
{
    public Task AddRange(List<ReservationAddition> list);
}
