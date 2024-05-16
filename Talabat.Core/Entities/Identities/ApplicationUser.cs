using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identities
{
	public class ApplicationUser : IdentityUser/*<string>*/
	{
        public string DisplayName { get; set; } = null!;
        public Address? Address { get; set; }

    }
}
