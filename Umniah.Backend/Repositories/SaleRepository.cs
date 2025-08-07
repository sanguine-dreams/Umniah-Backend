using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class SaleRepository(UmniahDbContext dbContext) : ICrudRepository<Sale, Sale>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task<ServiceResponse<bool>> Create(Sale input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Sale>> Edit(Guid id, Sale input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Sale>> Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Sale>> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Sale>(id))!;
    }

    public async Task<ServiceResponse<List<Sale>>> GetAll()
    {
        return  await _dbContext.Sales.ToListAsync();
    }
}