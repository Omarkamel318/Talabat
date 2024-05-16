using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Core.Contract
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

		Task<int> CompleteAsync();
	}
}
