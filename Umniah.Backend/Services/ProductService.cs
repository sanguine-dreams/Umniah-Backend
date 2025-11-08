using MapsterMapper;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class ProductService(ICrudRepository<Product> _productRepository, IMapper _mapper) : ICrudService<InputProduct, OutputProduct>
{
    public async Task<ServiceResponse<bool>> Create(InputProduct input)
    {
        var product = _mapper.Map<Product>(input);
        await _productRepository.CreateAsync(product);  
        return new ServiceResponse<bool>(true, "Product successfully created", true);
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
            return new ServiceResponse<bool>(false, "Product not found");

        try
        {
            await _productRepository.DeleteAsync(id);
            return new ServiceResponse<bool>(true, "Product successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting product: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, InputProduct input)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
            return new ServiceResponse<bool>(false, "Product not found");

        // Update properties
     
        try
        {
            await _productRepository.UpdateAsync(existingProduct);
            return new ServiceResponse<bool>(true, "Product successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating product: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<List<OutputProduct>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        var output = _mapper.Map<List<OutputProduct>>(products);
        return new ServiceResponse<List<OutputProduct>>(true, "Products retrieved successfully", output);
    }

    public async Task<ServiceResponse<OutputProduct>> GetById(Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
            return new ServiceResponse<OutputProduct>(false, "Product not found");
            var output =_mapper.Map<OutputProduct>(existingProduct);
        return new ServiceResponse<OutputProduct>(true, "Product retrieved successfully", output);
    }
}