using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class PurchaseRepository(UmniahDbContext dbContext) : ICrudRepository<Purchase, Purchase>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task Create(Purchase input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Guid id, Purchase input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Purchase> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Purchase>(id))!;
    }

    public async Task<List<Purchase>> GetAll()
    {
        return  await _dbContext.Purchases.ToListAsync();
    }
}