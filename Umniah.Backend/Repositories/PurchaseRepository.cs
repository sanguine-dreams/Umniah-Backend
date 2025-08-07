using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class PurchaseRepository(UmniahDbContext dbContext) : ICrudRepository<Purchase, Purchase>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task<ServiceResponse<bool>> Create(Purchase input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Purchase>> Edit(Guid id, Purchase input)
    {
        _dbContext.Update(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Purchase>> Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<Purchase>> GetById(Guid id)
    {
        return (await _dbContext.FindAsync<Purchase>(id))!;
    }

    public async Task<ServiceResponse<List<Purchase>>> GetAll()
    {
        return  await _dbContext.Purchases.ToListAsync();
    }
}