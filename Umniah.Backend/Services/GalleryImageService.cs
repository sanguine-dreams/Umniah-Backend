using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class GalleryImageService (ICrudRepository<GalleryImage> _galleryRepository, IMapper _mapper) : ICrudService<InputGalleryImage, OutputGalleryImage>
{
 public async Task<ServiceResponse<bool>> Create(InputGalleryImage input)
    {
        
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
    
        var result = _mapper.Map<GalleryImage>(input);
        try
        {
            await _galleryRepository.CreateAsync(result);
            return new ServiceResponse<bool>(true, "Image successfully created", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error saving image: {ex.Message}");
        }
    }
    public async Task Edit(Guid id, InputGalleryImage input)
    {
        
    }

    public async Task Delete(Guid id)
    {
      
    }

    public async Task<ServiceResponse<OutputGalleryImage>> GetById(Guid id)
    {
       return (await _dbContext.FindAsync<GalleryImage>(id))!;
    }

    public async Task<ServiceResponse<List<OutputGalleryImage>>> GetAll()
    {
        return  await _dbContext.GalleryImages.ToListAsync();
     
    }

  
}