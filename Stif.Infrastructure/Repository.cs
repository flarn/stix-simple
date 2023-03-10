using Microsoft.EntityFrameworkCore;
using Stix.Core.Entities;

namespace Stix.Infrastructure
{
    public class Repository<T> where T : EntityBase
    {
        private readonly StixDbContext _stixDbContext;

        public Repository(StixDbContext stixDbContext)
        {
            _stixDbContext = stixDbContext;
        }


        protected async Task<IReadOnlyList<T>> GetAll()
        {
            return await _stixDbContext.Set<T>().ToListAsync();
        }

        protected async Task<T?> GetById(string id)
        {
            return await _stixDbContext.Set<T>().FindAsync(id);
        }

        protected async Task Delete(T entity)
        {
            _stixDbContext.Set<T>().Remove(entity);

            await _stixDbContext.SaveChangesAsync();
        }

        protected async Task<T> Create(T entity)
        {
            _stixDbContext.Set<T>().Add(entity);
            await _stixDbContext.SaveChangesAsync();
            return entity;
        }
    }
}