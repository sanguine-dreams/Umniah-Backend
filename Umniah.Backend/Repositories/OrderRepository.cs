using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class OrderRepository(UmniahDbContext dbContext) : ICrudRepository<Order, Order>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public Task Create(Order input)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Guid id, Order input)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAll()
    {
        throw new NotImplementedException();
    }
}