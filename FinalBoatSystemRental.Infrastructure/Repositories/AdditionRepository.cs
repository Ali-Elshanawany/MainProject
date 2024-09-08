



using FinalBoatSystemRental.Core.ViewModels.Addition;

namespace FinalBoatSystemRental.Infrastructure.Repositories;

public class AdditionRepository : BaseRepository<Addition>, IAdditionRepository
{


    private readonly ApplicationDbContext _dbcontext;
    public AdditionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<List<GetPriceAdditionViewModel>> GetAdditionPrice(List<int> ids)
    {
        return await _dbcontext.Additions
            .Where(a => ids.Contains(a.Id))
            .Select(b => new GetPriceAdditionViewModel
            {
                Id = b.Id,
                price = b.Price
            })
            .ToListAsync();
    }

    public async Task<bool> CheckAdditionName(string name, int ownerId)
    {
        return await _dbcontext.Additions.AnyAsync(a => a.Name == name && a.Owner.Id == ownerId);
    }
}
