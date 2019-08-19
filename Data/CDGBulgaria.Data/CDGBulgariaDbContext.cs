using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CDGBulgaria.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Data
{
	public class CDGBulgariaDbContext: IdentityDbContext<CDGUser, IdentityRole, string>
	{

		public DbSet<Article> Articles { get; set; }

		public DbSet<Question> Questions { get; set; }

		public DbSet<Answer> Answers { get; set; }

		public DbSet<Event> Events { get; set; }

		public DbSet<CDGDisease> CDGDiseases { get; set; }

		public DbSet<Fact> Facts { get; set; }

		public CDGBulgariaDbContext(DbContextOptions options)
			:base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
				
				
			base.OnModelCreating(builder);
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database = CDGBulgariaDB; Trusted_Connection = True;");

		//	base.OnConfiguring(optionsBuilder);
		//}

		
	}
}
