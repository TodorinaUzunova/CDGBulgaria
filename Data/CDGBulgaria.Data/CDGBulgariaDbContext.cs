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

		public DbSet<Reply> Replies { get; set; }

		public DbSet<Event> Events { get; set; }

		public DbSet<CDGDesease> Deseases { get; set; }

		public CDGBulgariaDbContext(DbContextOptions options)
			:base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
				
				
			base.OnModelCreating(builder);
		}
	}
}
