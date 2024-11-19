using Umniah.Backend.Data;

namespace Umniah.Backend.Interfaces;

public interface ICrudRepository<TOutput, TInput> where TInput : BaseModel
{
    Task Create(TInput input );
    Task Edit(Guid id, TInput input );
    Task Delete(Guid id );
    Task<TOutput> GetById(Guid id);
    Task<List<TOutput>> GetAll();
}