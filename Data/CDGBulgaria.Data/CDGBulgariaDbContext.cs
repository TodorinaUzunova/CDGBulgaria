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

			//builder.Entity<Question>()
			//	.HasMany(q => q.Answers)
			//	.WithOne(a => a.Question)
			//	.HasForeignKey(a=>a.QuestionId)
			//	.OnDelete(DeleteBehavior.Cascade);

			//builder.Entity<Answer>()
			//	.HasOne(a => a.Question)
			//	.WithMany(q=>q.Answers)
			//	.HasForeignKey(a=>a.QuestionId)
			//	.OnDelete(DeleteBehavior.Restrict);
				
				
			base.OnModelCreating(builder);
		}
	}
}
