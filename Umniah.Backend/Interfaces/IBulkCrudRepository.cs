using System.Reflection.Metadata.Ecma335;
using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface IBulkCrudRepository <TOutput, TInput>
{
    Task EditAll(List<TInput> input);
    Task<ServiceResponse<List<TOutput>>> GetAll();
}