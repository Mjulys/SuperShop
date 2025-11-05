using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace SuperShop.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        public readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await SaveAllAsync();
                
        }
        public async Task UpdateAsync(T entity)
        {

            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }

         public async Task DeleteAsync(T entity)
        {
           _context.Set<T>().Remove(entity);
           await SaveAllAsync();
                
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
