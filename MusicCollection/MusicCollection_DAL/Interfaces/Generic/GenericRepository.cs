using Microsoft.EntityFrameworkCore;
using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_DAL.Interfaces.Generic
{
    public class GenericRepository : IGeneric
    {
        private readonly SpotifyContext context;
        public GenericRepository(SpotifyContext Context)
        {
            if (Context == null) throw new ArgumentNullException("Context");
            this.context = Context;
        }


        public async Task<bool> Add<T>(T entity) where T : M_DbEntity
        {
            entity.Id = default(int);
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete<T>(int id) where T : M_DbEntity
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            if (entity == null) return false;
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete<T>(T entity) where T : M_DbEntity
        {
            return await Delete<T>(entity.Id);
        }

        public async Task<T?> GetById<T>(int id) where T : M_DbEntity
        {
            try
            {
                return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex) { }
            return null;
        }

        public async Task<TEntity> GetFirstAsync<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : M_DbEntity
        {
            DbSet<TEntity> entities = context.Set<TEntity>();
            Type entityType = typeof(TEntity);
            PropertyInfo? property = entityType.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist in entity '{entityType.Name}'.", nameof(propertyName));
            }

            IQueryable<TEntity> filteredEntities = entities.Where(e => EF.Property<TValue>(e, propertyName)!.Equals(propertyValue));

            return await filteredEntities.FirstOrDefaultAsync();
        }

        public async Task<ICollection<T>> Get<T>() where T : M_DbEntity
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<ICollection<T>> Get<T>(int page, int entitiesPerPage = 10) where T : M_DbEntity
        {
            if (page <= 0)
                throw new ArgumentException("Page number must be greater than zero.", nameof(page));

            if (entitiesPerPage <= 0)
                throw new ArgumentException("Entities per page must be greater than zero.", nameof(entitiesPerPage));

            return await context.Set<T>().Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage).ToListAsync();
        }
        public async Task<ICollection<TEntity>> Get<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : M_DbEntity
        {
            DbSet<TEntity> entities = context.Set<TEntity>();
            Type entityType = typeof(TEntity);
            PropertyInfo? property = entityType.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist in entity '{entityType.Name}'.", nameof(propertyName));
            }

            IQueryable<TEntity> filteredEntities = entities.Where(e => EF.Property<TValue>(e, propertyName)!.Equals(propertyValue));

            return await filteredEntities.ToListAsync();
        }
        public async Task<ICollection<TEntity>> Get<TEntity>(Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : M_DbEntity
        {
            DbSet<TEntity> entities = context.Set<TEntity>();
            IQueryable<TEntity> filteredEntities = entities;
            Type entityType = typeof(TEntity);

            foreach (var filter in propertyFilters)
            {
                PropertyInfo? property = entityType.GetProperty(filter.Key);

                if (property == null)
                {
                    throw new ArgumentException($"Property '{filter.Key}' does not exist in entity '{entityType.Name}'.", nameof(propertyFilters));
                }

                if (property.PropertyType == typeof(string) && !filter.Value.isExactMatch)
                {
                    string filterValue = (string)filter.Value.value;
                    filteredEntities = filteredEntities.Where(e => EF.Property<string>(e, filter.Key).Contains(filterValue));
                }
                else
                {
                    filteredEntities = filteredEntities.Where(e => EF.Property<object>(e, filter.Key)!.Equals(filter.Value.value));
                }
            }

            return await filteredEntities.ToListAsync();
        }



        public async Task<bool> Update<T>(int id, T entity) where T : M_DbEntity
        {
            try
            {
                T? _entity = await context.Set<T>().FindAsync(id);
                if (_entity == null) return false;
                entity.Id = _entity.Id;
                context.Entry(_entity).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //Console.WriteLine($"An error occurred while updating entity: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Update<T>(T oldEntity, T newEntity) where T : M_DbEntity
        {
            return await Update(oldEntity.Id, newEntity);
        }
    }
}
