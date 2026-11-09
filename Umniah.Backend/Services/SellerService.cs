using MapsterMapper;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class SellerService(IBulkRepository<Seller> _sellerRepository, IMapper _mapper) : IBulkCrudService<InputSeller, OutputSeller>
{
    public async Task<ServiceResponse<bool>> EditAll(List<InputSeller> input)
    {
        var sellers = _mapper.Map<List<Seller>>(input);
        await _sellerRepository.EditAll(sellers); 
         return new ServiceResponse<bool>(true, "Sellers updated successfully");
    }

    public async Task<ServiceResponse<List<OutputSeller>>> GetAll()
    {
        var sellers = await _sellerRepository.GetAll();
        var output = _mapper.Map<List<OutputSeller>>(sellers);   
        return new ServiceResponse<List<OutputSeller>>(true, "Sellers retrieved successfully", output);
    }

}