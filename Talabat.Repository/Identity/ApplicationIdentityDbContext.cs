using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Infrastructure.Identity
{
	public class ApplicationIdentityDbContext : IdentityDbContext
	{
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Address>().ToTable("Addresses");
		}

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
    }
}
