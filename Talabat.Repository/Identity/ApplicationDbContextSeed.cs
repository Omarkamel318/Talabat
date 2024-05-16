using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Infrastructure.Identity
{
	public static class ApplicationDbContextSeed
	{
		public async static Task UsersSeedAsync(UserManager<ApplicationUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					UserName = "Omar",
					PhoneNumber = "01144710999",
					Email = "omarkamel318@gmail.com"
				};
				await userManager.CreateAsync(user, "P@ssw0rd"); 
			}
		}

	}
}