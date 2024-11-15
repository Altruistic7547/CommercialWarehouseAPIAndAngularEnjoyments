using MyWarehouse.Application.Common.Mapping;
using MyWarehouse.Domain.Common;
using System.Linq.Expressions;

namespace MyWarehouse.Application.Common.Dependencies.DataAccess.Repositories.Common;

public interface IRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Returns the entity corresponding to the given ID, or default if not found.
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Returns the enumarable of entity based on the filter.
    /// </summary>
    Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter, bool readOnly = false);

    /// <summary>
    /// Inserts the entity record.
    /// </summary>
    void Add(TEntity entity);

    /// <summary>
    /// Bulk inserts the entity records.
    /// </summary>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Removes the entity record.
    /// </summary>
    void Remove(TEntity entity);

    /// <summary>
    /// Bulk removes the entity record.
    /// </summary>
    void RemoveRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Begins tracking the given entity.
    /// </summary>
    void StartTracking(TEntity entity);

    /// <summary>
    /// Finds the entity of the given Id, and returns it mapped to the specified mappable type, or returns default if not found.
    /// </summary>
    Task<TDto?> GetProjectedAsync<TDto>(int id, bool readOnly = false) where TDto : IMapFrom<TEntity>;

    /// <summary>
    /// Finds the list of entities corresponding to the provided query, and returns them mapped to the specified mappable type.
    /// </summary>
    Task<IListResponseModel<TDto>> GetProjectedListAsync<TDto>(ListQueryModel<TDto> model, Expression<Func<TEntity, bool>>? additionalFilter = null, bool readOnly = false) where TDto : IMapFrom<TEntity>;
}
