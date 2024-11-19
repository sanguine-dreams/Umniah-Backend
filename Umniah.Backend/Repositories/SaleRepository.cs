using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class SaleRepository(UmniahDbContext dbContext) : ICrudRepository<Sale, Sale>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public Task Create(Sale input)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Guid id, Sale input)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Sale> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Sale>> GetAll()
    {
        throw new NotImplementedException();
    }
}