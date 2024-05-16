using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.IRepositories
{
	public interface IGenericRepository<T> where T : EntityBase
	{
		public Task<IReadOnlyList<T>> GetAllAsync();

		public Task<T?> GetAsync(int id);

		public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		public Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

		public Task<int> CountAsync(ISpecifications<T> spec);

		void Add(T entity);

		void Update(T entity);

		void Delete(T entity);
	}
}
