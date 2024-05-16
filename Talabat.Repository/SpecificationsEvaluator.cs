using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : EntityBase
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> entity , ISpecifications<TEntity> spec)
		{
			var query = entity;
			if (spec is not null)
			{
				if (spec.Criteria is not null)
					query = query.Where(spec.Criteria);

				if(spec.OrderBy is not null)
					query = query.OrderBy(spec.OrderBy);

				else if(spec.OrderByDesc is not null)
					query = query.OrderByDescending(spec.OrderByDesc);

				if(spec.IsPaginationEnabled)
				{
					query = query.Skip(spec.Skip).Take(spec.Take);	
				}

				
				//foreach (var include in spec.Includes)
				//{
				//	query = query.Include(include);
				//} 
			 query = spec.Includes.Aggregate(query , (currentQuery,nextQuery) => currentQuery.Include(nextQuery));
			}
			return query;
		}
	}
}
