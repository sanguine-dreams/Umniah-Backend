using Umniah.Backend.Data;
using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface ICrudService<TInput, TOutput>
{
    Task<ServiceResponse<bool>> Create(TInput input );
   Task<ServiceResponse<bool>> Edit(Guid id, TInput input );
    Task<ServiceResponse<bool>> Delete(Guid id );
    Task<ServiceResponse<TOutput>> GetById(Guid id);
    Task<ServiceResponse<List<TOutput>>> GetAll();
}