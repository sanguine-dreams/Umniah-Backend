using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface IBulkCrudService <TInput, TOutput>
{
    Task<ServiceResponse<bool>>  EditAll(List<TInput> input);
    Task<ServiceResponse<List<TOutput>>> GetAll();
}