using Umniah.Backend.Data;

namespace Umniah.Backend.Interfaces;
public interface IBulkRepository<TEntity> where TEntity : BaseModel
{
    Task EditAll(List<TEntity> input);
    Task<List<TEntity>> GetAll();
}