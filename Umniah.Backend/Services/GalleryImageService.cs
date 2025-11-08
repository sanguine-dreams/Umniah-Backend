using MapsterMapper;
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
    public async Task<ServiceResponse<bool>> Edit(Guid id, InputGalleryImage input)
    {
        var existingImage = await _galleryRepository.GetByIdAsync(id);
        if (existingImage == null)
            return new ServiceResponse<bool>(false, "Gallery image not found");
    
        // Update properties
        existingImage.Caption = input.Caption;
        existingImage.Tags = input.Tags;
    
        try
        {
            await _galleryRepository.UpdateAsync(existingImage);
            var output = _mapper.Map<OutputGalleryImage>(existingImage);
            return new ServiceResponse<bool>(true, "Image successfully updated");
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating image: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
      var existingImage = await _galleryRepository.GetByIdAsync(id);
        if (existingImage == null)
            return new ServiceResponse<bool>(false, "Gallery image not found");
    
        try
        {
            await _galleryRepository.DeleteAsync(id);
            var output = _mapper.Map<OutputGalleryImage>(existingImage);
            return new ServiceResponse<bool>(true, "Image successfully deleted");
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting image: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OutputGalleryImage>> GetById(Guid id)
    {
        var existingImage = await _galleryRepository.GetByIdAsync(id);
        if (existingImage == null)
            return new ServiceResponse<OutputGalleryImage>(false, "Gallery image not found");
    
        var output = _mapper.Map<OutputGalleryImage>(existingImage);
        return new ServiceResponse<OutputGalleryImage>(true, "Image retrieved successfully", output);
    }

    public async Task<ServiceResponse<List<OutputGalleryImage>>> GetAll()
    {
       var images = await _galleryRepository.GetAllAsync();
        var output = _mapper.Map<List<OutputGalleryImage>>(images);
        return new ServiceResponse<List<OutputGalleryImage>>(true, "Images retrieved successfully", output);
     
    }
}