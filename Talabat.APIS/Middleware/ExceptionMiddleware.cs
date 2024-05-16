using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Talabat.APIS.Error;

namespace Talabat.APIS.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IWebHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}
        public async Task InvokeAsync(HttpContext context)
		{
			// execution in forward
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				// execution in backword
				_logger.LogError(ex.Message);
				context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
				context.Response.ContentType="application/json";

				var response = _env.IsDevelopment() ? 
					new APIExceptionErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message)
					: 
					new APIExceptionErrorResponse((int)HttpStatusCode.InternalServerError);
				var json =JsonSerializer.Serialize(response);

				await context.Response.WriteAsync(json);

			}

		}
	}
}
