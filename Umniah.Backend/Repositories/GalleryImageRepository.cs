using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class GalleryImageRepository(UmniahDbContext dbContext) : ICrudRepository<GalleryImage, GalleryImage>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public Task Create(GalleryImage input)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Guid id, GalleryImage input)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<GalleryImage> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GalleryImage>> GetAll()
    {
        throw new NotImplementedException();
    }
}