using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface IBulkCrudService <TOutput, TInput>
{
    Task EditAll(List<TInput> input);
    Task<ServiceResponse<List<TOutput>>> GetAll();
}