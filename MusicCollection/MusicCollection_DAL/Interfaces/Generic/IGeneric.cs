using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces.Generic
{
    public interface IGeneric
    {
        Task<bool> Add<T>(T entity) where T : M_DbEntity;
        Task<bool> Delete<T>(int id) where T : M_DbEntity;
        Task<bool> Delete<T>(T entity) where T : M_DbEntity;

        Task<bool> Update<T>(int id, T entity) where T : M_DbEntity;
        Task<bool> Update<T>(T oldEntity, T newEntity) where T : M_DbEntity;

        Task<T?> GetById<T>(int id) where T : M_DbEntity;
        Task<TEntity> GetFirstAsync<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : M_DbEntity;

        Task<ICollection<T>> Get<T>() where T : M_DbEntity;
        Task<ICollection<T>> Get<T>(int page = 1, int entitiesPerPage = 10) where T : M_DbEntity;
        Task<ICollection<TEntity>> Get<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : M_DbEntity;
        Task<ICollection<TEntity>> Get<TEntity>(Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : M_DbEntity;
    }
}
