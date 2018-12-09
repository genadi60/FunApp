using System;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FunApp.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
    where TEntity : class
    {
        private readonly FunAppContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public DbRepository(FunAppContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public IQueryable<TEntity> All()
        {
            return _dbSet;
        }

        public Task AddAsync(TEntity entity)
        {
           return _dbSet.AddAsync(entity); 
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
