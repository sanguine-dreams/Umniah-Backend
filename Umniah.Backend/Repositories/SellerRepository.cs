using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class SellerRepository(UmniahDbContext dbContext) : IBulkCrudRepository<Seller, Seller>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task EditAll(List<Seller> input)
    {
        // Iterate through the list of sellers
        foreach (var seller in input)
        {
            // Try to find the existing seller in the database by ID
            var existingSeller = await _dbContext.Sellers.FindAsync(seller.Id);

            if (existingSeller != null)
            {
                // Update the existing seller's properties
                existingSeller.Name = seller.Name;
                existingSeller.Address = seller.Address;
                existingSeller.Contact = seller.Contact;
                // Add more fields as necessary

                // Mark the entity as modified
                _dbContext.Entry(existingSeller).State = EntityState.Modified;
            }
            else
            {
                // If the seller doesn't exist, add it as a new seller
                await _dbContext.Sellers.AddAsync(seller);
            }
        }

        // Save all changes to the database
        await _dbContext.SaveChangesAsync();
    
    }

    public async Task<List<Seller>> GetAll()
    {
        return  await _dbContext.Sellers.ToListAsync();
    }
}