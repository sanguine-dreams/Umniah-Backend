using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class PurchaseRepository(UmniahDbContext dbContext) : ICrudRepository<Purchase, Purchase>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public Task Create(Purchase input)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Guid id, Purchase input)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetAll()
    {
        throw new NotImplementedException();
    }
}