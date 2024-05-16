using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Identities;

namespace Talabat.Application
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(ApplicationUser user , UserManager<ApplicationUser> userManager)
		{
			#region Pricate Claims
			var userClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.Email,user.Email)
			};

			var roles = await userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				userClaims.Add(new Claim(ClaimTypes.Role, role));
			} 
			#endregion

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:AuthKey"]));

			var token = new JwtSecurityToken
				(   //registered claims
					issuer: _configuration["Jwt:Issure"],
					audience: _configuration["Jwt:Audiance"],
					expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:exp"] ?? "0")),
					//private claims
					claims: userClaims,
					//signiture
					signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)

				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
