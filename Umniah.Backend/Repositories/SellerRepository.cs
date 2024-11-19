using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class SellerRepository(UmniahDbContext dbContext) : IBulkCrudRepository<Seller, Seller>
{
    public Task EditAll(List<Seller> input)
    {
        throw new NotImplementedException();
    }

    public Task<List<Seller>> GetAll()
    {
        throw new NotImplementedException();
    }
}