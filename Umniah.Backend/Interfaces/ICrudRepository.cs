using Umniah.Backend.Data;

namespace Umniah.Backend.Interfaces;


public interface ICrudRepository<TEntity> where TEntity : BaseModel
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}