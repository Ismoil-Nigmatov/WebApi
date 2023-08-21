﻿using project.Entity;

namespace project.Generics
{
    public interface IGenericService<T> where T :class , IEntity
    {
        Task<List<T>> GetAll();
        Task<T?> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}