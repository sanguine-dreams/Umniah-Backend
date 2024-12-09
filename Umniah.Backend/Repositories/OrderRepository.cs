using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class OrderRepository(UmniahDbContext dbContext) : ICrudRepository<Order, Order>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task Create(Order input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Guid id, Order input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Order>(id))!;
    }

    public async Task<List<Order>> GetAll()
    {
        return  await _dbContext.Orders.ToListAsync();
    }
}