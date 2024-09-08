

namespace FinalBoatSystemRental.Core.Interfaces;

public interface IBoatRepository:IBaseRepository<Boat>
{

    Task<Boat> GetByIdAsync(int ownerid,int id);

    Task<IEnumerable<Boat>> GetAllAsync(int ownerid);
    Task<IEnumerable<Boat>> GetAllAvailableBoatsAsync();
    Task <bool> CheckBoatName(int id , string name);
    public Task<decimal> GetReservationBoatPrice(int id);

    public  Task<int> GetMaxCancellationDateInDays(int boatId);

    // Check If the User Is really The Owner Of The Boat 
    Task<bool> IsOwner(int boatId,int ownerid);



   


}
