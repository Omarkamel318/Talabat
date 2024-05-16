using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.APIS.Error;
using Talabat.APIS.Helpers;
using Talabat.Application;
using Talabat.Core.Contract;
using Talabat.Core.IRepositories;
using Talabat.Infrastructure;
using Talabat.Repository;

namespace Talabat.APIS.Extensions
{
	public static class ApplicatinServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(IOrderService), typeof(OrderService));
			services.AddScoped(typeof(IProductService), typeof(ProductService));

			services.AddAutoMapper(typeof(MappingProfile));             //for mapper
			services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
								.SelectMany(p => p.Value.Errors)
								.Select(e => e.ErrorMessage)
								.ToList();
					var response = new APiValidationErrorResponse(errors);
					return new BadRequestObjectResult(response);

				};
			});

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			services.AddScoped(typeof(IAuthService),typeof(AuthService));	


			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}

	
	}
}
