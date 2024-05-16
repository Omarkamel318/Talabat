using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Contract;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _context;
		private Hashtable _repositories;
		public UnitOfWork(StoreContext context)
        {
			_context = context;
			_repositories = new Hashtable();
		}
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
		{
			var key = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(key))
				_repositories.Add(key, new GenericRepository<TEntity>(_context));

			return _repositories[key] as IGenericRepository<TEntity>;
		}

		public Task<int> CompleteAsync()
			=>_context.SaveChangesAsync();


		public ValueTask DisposeAsync()
			=>_context.DisposeAsync();

	}
}
