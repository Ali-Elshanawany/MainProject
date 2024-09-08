


namespace FinalBoatSystemRental.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{

    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;
    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, TEntity entity)
    {
        var result = await _dbSet.FindAsync(id);
        if (result != null)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(result);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Not Found");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data != null)
        {
            _dbSet.Remove(data);
            await _dbContext.SaveChangesAsync();
        }

    }


}
