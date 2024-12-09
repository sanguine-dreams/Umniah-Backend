using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class SaleRepository(UmniahDbContext dbContext) : ICrudRepository<Sale, Sale>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task Create(Sale input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Guid id, Sale input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Sale> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Sale>(id))!;
    }

    public async Task<List<Sale>> GetAll()
    {
        return  await _dbContext.Sales.ToListAsync();
    }
}