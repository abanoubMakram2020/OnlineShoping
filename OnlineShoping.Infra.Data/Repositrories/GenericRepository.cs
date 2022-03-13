using Microsoft.EntityFrameworkCore;
using OnlineShoping.Domain.RepositoryInterfaces;
using OnlineShoping.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace OnlineShoping.Infra.Data.Repositrories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly OnlineShopingDBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(OnlineShopingDBContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(OnlineShopingDBContext));
            _dbSet = _context.Set<T>();
        }


        public async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression = null, string includes = "")
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includes))
                query = includes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, include) => current.Include(include));

            if (expression == null)
                return query;
            else
                return query.Where(expression);
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Add(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Delete(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(List<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
