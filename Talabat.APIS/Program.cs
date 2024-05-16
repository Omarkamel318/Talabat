using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIS.Error;
using Talabat.APIS.Extensions;
using Talabat.APIS.Helpers;
using Talabat.APIS.Middleware;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identities;
using Talabat.Core.IRepositories;
using Talabat.Infrastructure.Data.DataSeeding;
using Talabat.Infrastructure.Identity;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIS
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddApplicationServices(); //created by me
			builder.Services.AddSwagger(); //created by me

			builder.Services.AddDbContext<StoreContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("default"))
				);
			builder.Services.AddDbContext<ApplicationIdentityDbContext>(option=>
				option.UseSqlServer(builder.Configuration.GetConnectionString("identity"))
				);

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationIdentityDbContext>();

			builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
				 ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")));

			builder.Services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme // add scheme for bearer*/option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				
			}
			)
				.AddJwtBearer(option =>
			option.TokenValidationParameters = new TokenValidationParameters()
			{	
				ValidateIssuer = true,
				ValidIssuer = builder.Configuration["Jwt:Issure"],
				ValidateAudience = true,
				ValidAudience = builder.Configuration["Jwt:Audiance"],
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:AuthKey"])),
				ClockSkew = TimeSpan.Zero

			}) ;
			

			//validation error returned by clr implicitly


			var app = builder.Build();
			// Configure the HTTP request pipeline.

			#region Apply Migration and seeding
			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbcontext = services.GetRequiredService<StoreContext>();

			var _identityDbcontext = services.GetRequiredService<ApplicationIdentityDbContext>();

			var _userManager =services.GetRequiredService<UserManager<ApplicationUser>>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbcontext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbcontext);

				await _identityDbcontext.Database.MigrateAsync();
				await ApplicationDbContextSeed.UsersSeedAsync(_userManager);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "error occur during migration");
			} 
			#endregion




			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseMiddleware<ExceptionMiddleware>();

			app.UseHttpsRedirection();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}");// ReExecute if status code between 400:599

			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
