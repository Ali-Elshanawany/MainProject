

namespace FinalBoatSystemRental.Core.Interfaces;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<int> GetCustomerIdByUserId(string userId);

    public Task<Customer> GetCustomerByUserId(string userId);
    public Task<bool> CheckCustomerBalance(int customerId, decimal totalPrice);
}
