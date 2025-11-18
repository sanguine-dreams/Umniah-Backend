using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class Repository<TEntity> : ICrudRepository<TEntity>, IBulkRepository<TEntity>
    where TEntity : BaseModel
{
    protected readonly UmniahDbContext _context;
    protected readonly DbSet<TEntity> _set;

    public Repository(UmniahDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        => await _set.FindAsync(id);

    public virtual async Task<List<TEntity>> GetAllAsync()
        => await _set.ToListAsync();

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        _set.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _set.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

      public async Task EditAll(List<TEntity> input)
    {
        _context.UpdateRange(input);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _set.ToListAsync();
    }

    public Task<TEntity> GetByName(string name)
    {
        var result = _set.AsQueryable().Where(e => EF.Property<string>(e, "Name") == name).FirstOrDefaultAsync();
        return result;
    }
}