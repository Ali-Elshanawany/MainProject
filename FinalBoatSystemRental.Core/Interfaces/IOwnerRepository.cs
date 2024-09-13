

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IOwnerRepository : IBaseRepository<Owner>
{

    Task<Owner> GetByUserId(string userId);

    Task<int> GetOwnerIdByUserId(string userId);

    public Task<IEnumerable<Owner>> GetAllPendingOwners();

}
