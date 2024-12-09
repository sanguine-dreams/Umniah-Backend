using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class ProductRepository(UmniahDbContext dbContext) : ICrudRepository<Product, Product>
{
    private readonly UmniahDbContext _dbContext = dbContext;


    public async Task Create(Product input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Guid id, Product input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Product>(id))!;
    }

    public async Task<List<Product>> GetAll()
    {
        return  await _dbContext.Products.ToListAsync();    }
}