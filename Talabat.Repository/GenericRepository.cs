using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Infrastructure;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			///if(typeof(T) == typeof(Product))
			///{
			///	return (IEnumerable<T>) /*why*/ await _dbContext.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).ToListAsync();
			///}
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			///if (typeof(T) == typeof(Product))
			///{
			///	return await _dbContext.Set<Product>().Where(p=>p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T /*why*/;
			///}
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySepecification(spec).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySepecification(spec).FirstOrDefaultAsync();
		}

		public async Task<int> CountAsync(ISpecifications<T> spec)
		{
			return await ApplySepecification(spec).CountAsync();
		}


		private IQueryable<T> ApplySepecification(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
		}

		public void Add(T entity)
			=> _dbContext.Set<T>().Add(entity);

		public void Update(T entity)
			=> _dbContext.Set<T>().Update(entity);

		public void Delete(T entity)
			=> _dbContext.Set<T>().Remove(entity);

	}
}
