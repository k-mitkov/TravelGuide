﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelGuide.Database.Interfaces;

namespace TravelGuide.Database.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region Declarations

        protected TravelGuideContext database;
        protected DbSet<TEntity> entities;
        protected object lockObject;

        #endregion

        #region Constructor 

        public GenericRepository(object lockObject, TravelGuideContext database)
        {
            this.lockObject = lockObject;
            this.database = database;
            entities = database.Set<TEntity>();
        }

        #endregion

        #region Methods

        public async Task<TEntity> Insert(TEntity entity)
        {
            lock (lockObject)
            {
                if (entity != null)
                {
                    entity.Id = null;
                    entities.Add(entity);
                }
            }

            await database.SaveChangesAsync();

            return await Task.Run(() => { return entity; });
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            lock (lockObject)
            {
                if (entity != null)
                {
                    entities.Update(entity);
                }
            }
            await database.SaveChangesAsync();

            return await Task.Run(() => { return entity; });
        }
        public async Task<TEntity> Delete(TEntity entity)
        {
            lock (lockObject)
            {
                if (entity != null)
                {
                    entities.Remove(entity);
                }
            }
            await database.SaveChangesAsync();

            return await Task.Run(() => { return entity; });
        }

        public async Task DeleteByCondition(Expression<Func<TEntity, bool>> where = null)
        {
            lock (lockObject)
            {
                if (where != null)
                {
                    entities.RemoveRange(entities.Where(where));
                }
            }
            await database.SaveChangesAsync();
        }

        public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> where = null)
        {
            TEntity selectedEntity = new TEntity();

            lock(lockObject)
            {
                if (where != null)
                {
                    selectedEntity = entities.Where(where).FirstOrDefault();
                }
            }
            return await Task.Run(() => { return selectedEntity; });
        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync(Expression<Func<TEntity, bool>> where = null)
        {
            List<TEntity> selectedEntities = new List<TEntity>();

            lock (lockObject)
            {
                if (where != null)
                {
                    selectedEntities = entities.Where(where).ToList();
                }
                else
                {
                    selectedEntities = entities.ToList();
                }
            }

            return await Task.Run(() => { return selectedEntities; });
        }

        public void Dispose()
        {
            database.Dispose();
        }

        #endregion
    }
}
