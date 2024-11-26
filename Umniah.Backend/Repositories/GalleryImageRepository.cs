using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class GalleryImageRepository(UmniahDbContext dbContext) : ICrudRepository<GalleryImage, GalleryImage>
{
    private readonly UmniahDbContext _dbContext = dbContext;

    public async Task Create(GalleryImage input)
    {
        await _dbContext.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Guid id, GalleryImage input)
    {
         _dbContext.Update(input);
      await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GalleryImage> GetById(Guid id)
    {
       return (await _dbContext.FindAsync<GalleryImage>(id))!;
    }

    public async Task<List<GalleryImage>> GetAll()
    {
        return  await _dbContext.GalleryImages.ToListAsync();
     
    }
}