using MapsterMapper;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class SaleService(ICrudRepository<Sale> _saleRepository, IMapper _mapper) : ICrudService<InputSale, OutputSale>
{
    public async Task<ServiceResponse<bool>> Create(InputSale input)
    {
        var sale = _mapper.Map<Sale>(input);

        await _saleRepository.CreateAsync(sale);
        return new ServiceResponse<bool>(true, "Sale successfully created", true);
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        var existingSale = await _saleRepository.GetByIdAsync(id);
        if (existingSale == null)
            return new ServiceResponse<bool>(false, "Sale not found");

        try
        {
            await _saleRepository.DeleteAsync(id);
            return new ServiceResponse<bool>(true, "Sale successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting sale: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, InputSale input)
    {
        var existingSale = await _saleRepository.GetByIdAsync(id);
        if (existingSale == null)
            return new ServiceResponse<bool>(false, "Sale not found");

        // Update properties

        try
        {
            await _saleRepository.UpdateAsync(existingSale);
            return new ServiceResponse<bool>(true, "Sale successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating sale: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<List<OutputSale>>> GetAll()
    {
        var sales = await _saleRepository.GetAllAsync();
        var output = _mapper.Map<List<OutputSale>>(sales);
        return new ServiceResponse<List<OutputSale>>(true, "Sales retrieved successfully", output);
    }

    public async Task<ServiceResponse<OutputSale>> GetById(Guid id)
    {
    var existingSale = await _saleRepository.GetByIdAsync(id);
        if (existingSale == null)
            return new ServiceResponse<OutputSale>(false, "Sale not found");

        var output = _mapper.Map<OutputSale>(existingSale);
        return new ServiceResponse<OutputSale>(true, "Sale retrieved successfully", output);
    }
}