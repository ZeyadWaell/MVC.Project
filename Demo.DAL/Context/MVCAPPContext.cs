using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
	public class MVCAPPContext : IdentityDbContext<ApplicationUser> // this what i am going to make with it login registeration all of that stuff
	{
		public MVCAPPContext(DbContextOptions<MVCAPPContext> options) : base(options)
		{

		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("server = DESKTOP-BF349LE\\SQLEXPRESS; database = NewDatabase;integrated security = true");
		//}

		public DbSet<Employe> Departments { get; set; }
		public DbSet<Employe> Employes { get; set; }
	}
}
