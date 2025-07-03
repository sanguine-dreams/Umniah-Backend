using Umniah.Backend.Data;
using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface ICrudRepository<TInput, TOutput>
{
    Task<ServiceResponse<bool>> Create(TInput input );
    Task<ServiceResponse<TOutput>> Edit(Guid id, TInput input );
    Task<ServiceResponse<TOutput>> Delete(Guid id );
    Task<ServiceResponse<TOutput>> GetById(Guid id);
    Task<ServiceResponse<List<TOutput>>> GetAll();
}