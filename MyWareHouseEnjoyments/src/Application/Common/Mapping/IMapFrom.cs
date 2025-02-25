﻿namespace MyWarehouse.Application.Common.Mapping;

/// <summary>
/// Interface for DTOs, expressing that the DTO maps from a certain domain entity.
/// </summary>
public interface IMapFrom<TEntity>
{
    /// <summary>
    /// Default mapping implementation. If special mapping is required, 
    /// override this with an explicit Mapping() method declaration in the implementing DTO class.
    /// </summary>
    void Mapping(Profile profile) => profile.CreateMap(typeof(TEntity), GetType());
}
