using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Repositories;

public class GalleryImageRepository(UmniahDbContext dbContext) : ICrudRepository<InputGalleryImage, OutputGalleryImage>
{
    private readonly UmniahDbContext _dbContext = dbContext;
    
    public async Task<ServiceResponse<bool>> Create(InputGalleryImage input)
    {
        if (input is null)
            return new ServiceResponse<bool>(false, "Input is null");
    
        // Validate image file
        if (string.IsNullOrWhiteSpace(input.imageFile))
            return new ServiceResponse<bool>(false, "Image file is required");
    
        // Validate caption length (max 100 chars)
        if (string.IsNullOrWhiteSpace(input.Caption))
            return new ServiceResponse<bool>(false, "Caption is required");
    
        if (input.Caption.Length > 100)
            return new ServiceResponse<bool>(false, "Caption cannot exceed 100 characters");
    
        // Validate tags
        if (input.Tags == null || !input.Tags.Any())
            return new ServiceResponse<bool>(false, "At least one tag is required");
    
        // Validate image size (5MB max)
        try
        {
            var imageData = Convert.FromBase64String(input.imageFile);
            if (imageData.Length > 5 * 1024 * 1024)
                return new ServiceResponse<bool>(false, "Image size cannot exceed 5MB");
        }
        catch (FormatException)
        {
            return new ServiceResponse<bool>(false, "Invalid image format (must be valid base64)");
        }
    
        // Save to DB if validations pass
        try
        {
            await _dbContext.AddAsync(input);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Image successfully created", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error saving image: {ex.Message}");
        }
    }
    public async Task Edit(Guid id, InputGalleryImage input)
    {
         _dbContext.Update(input);
      await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbContext.Remove(id);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OutputGalleryImage> GetById(Guid id)
    {
       return (await _dbContext.FindAsync<GalleryImage>(id))!;
    }

    public async Task<List<OutputGalleryImage>> GetAll()
    {
        return  await _dbContext.GalleryImages.ToListAsync();
     
    }
}