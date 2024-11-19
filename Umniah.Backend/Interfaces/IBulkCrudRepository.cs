using System.Reflection.Metadata.Ecma335;

namespace Umniah.Backend.Interfaces;

public interface IBulkCrudRepository <TOutput, TInput>
{
    Task EditAll(List<TInput> input);
    Task<List<TOutput>> GetAll();
}