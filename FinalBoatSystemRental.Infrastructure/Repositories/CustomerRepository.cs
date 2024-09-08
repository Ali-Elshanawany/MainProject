using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Infrastructure.Repositories;

public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _dbcontext;
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }
    // Get Customer id By UserId 
    public async Task<int> GetCustomerIdByUserId(string userId)
    {
       return await _dbcontext.Customers
            .AsNoTracking()
            .Where(a=>a.UserId == userId)
            .Select(b=>b.Id)
            .FirstOrDefaultAsync();
    } 
    
    public async Task<Customer> GetCustomerByUserId(string userId)
    {

       return await _dbcontext.Customers
            .Where(a=>a.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckCustomerBalance(int customerId,decimal totalPrice)
    {
        return await _dbcontext.Customers
                             .AsNoTracking()
                             .AnyAsync(a => a.Id == customerId && a.WalletBalance >= totalPrice);
    }

    
}
