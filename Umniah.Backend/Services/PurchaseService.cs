using MapsterMapper;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class PurchaseService(ICrudRepository<Purchase> _purchaseRepository, IMapper _mapper) : ICrudService<InputPurchase, OutputPurchase>
{
    public async Task<ServiceResponse<bool>> Create(InputPurchase input)
    {
          var purchase = _mapper.Map<Purchase>(input);

        await _purchaseRepository.CreateAsync(purchase);
        return new ServiceResponse<bool>(true, "Purchase successfully created", true);
        }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
         var existingPurchase = await _purchaseRepository.GetByIdAsync(id);
        if (existingPurchase == null)
            return new ServiceResponse<bool>(false, "Purchase not found");

        try
        {
            await _purchaseRepository.DeleteAsync(id);
            return new ServiceResponse<bool>(true, "Purchase successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting purchase: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, InputPurchase input)
    {
        var existingPurchase = await _purchaseRepository.GetByIdAsync(id);
        if (existingPurchase == null)
            return new ServiceResponse<bool>(false, "Purchase not found");

        // Update properties

        try
        {
            await _purchaseRepository.UpdateAsync(existingPurchase);
            return new ServiceResponse<bool>(true, "Purchase successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating purchase: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<List<OutputPurchase>>> GetAll()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        var output = _mapper.Map<List<OutputPurchase>>(purchases);
        return new ServiceResponse<List<OutputPurchase>>(true, "Purchases retrieved successfully", output);
    }

    public async Task<ServiceResponse<OutputPurchase>> GetById(Guid id)
    {
        var existingPurchase = await _purchaseRepository.GetByIdAsync(id);
        if (existingPurchase == null)
            return new ServiceResponse<OutputPurchase>(false, "Purchase not found");

        var output = _mapper.Map<OutputPurchase>(existingPurchase);
        return new ServiceResponse<OutputPurchase>(true, "Purchase retrieved successfully", output);
    }
}

