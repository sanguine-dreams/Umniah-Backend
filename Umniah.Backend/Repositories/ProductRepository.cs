using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class ProductRepository(UmniahDbContext dbContext) : ICrudRepository<Product, Product>
{
    private readonly UmniahDbContext _dbContext = dbContext;


    public Task Create(Product input)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Guid id, Product input)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAll()
    {
        throw new NotImplementedException();
    }
}