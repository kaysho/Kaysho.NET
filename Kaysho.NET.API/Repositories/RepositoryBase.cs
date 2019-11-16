using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.Core.Models;
using DamilolaShopeyin.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DamilolaShopeyin.API.Repositories
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DamilolaShopeyinContext _context;

        protected RepositoryBase(DamilolaShopeyinContext context)
        {
            _context = context;
        }

        public virtual T Update(T entity)
        {
            var oldEntity = _context.Set<T>().Find(entity.Id);
            if (oldEntity != null)
            {
                _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            }

            return entity;
        }

        public virtual T Add(T entity)
        {
            _context.Add(entity);

            return entity;
        }

        public virtual T Delete(int? id)
        {
            var oldEntity = _context.Set<T>().Find(id);

            if (oldEntity != null)
            {
                _context.Remove(oldEntity);
            }

            return oldEntity;
        }

        public virtual T Get(int? id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<bool> Commit()
        {
            if (_context.ChangeTracker.HasChanges())
            {
                try
                {
                    return await _context.SaveChangesAsync() == 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }
            return false;
        }
    }
}
