

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IBoatBookingAdditionRepository : IBaseRepository<BoatBookingAddition>
{
    public Task AddRange(List<BoatBookingAddition> list);
}
