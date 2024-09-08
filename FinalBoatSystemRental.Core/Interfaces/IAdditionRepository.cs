
using FinalBoatSystemRental.Core.ViewModels.Addition;

namespace FinalBoatSystemRental.Core.Interfaces
{
    public interface IAdditionRepository : IBaseRepository<Addition>
    {

        public Task<List<GetPriceAdditionViewModel>> GetAdditionPrice(List<int> ids);

        // Check If Addition Name Is Already in DB For The Same Owner
        public Task<bool> CheckAdditionName(string name, int ownerId);
    }
}
