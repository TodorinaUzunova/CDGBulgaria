using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDGBulgaria.Services
{
	public class UsersService : IUsersService
	{
		private readonly CDGBulgariaDbContext context;

		public UsersService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}
		public bool CreateDoctor()
		{
			throw new NotImplementedException();
		}

		public IQueryable<CDGUserServiceModel> GetAllUsersByFullName()
		{
		  return this.context.Users.Select(u=>u.FullName).To<CDGUserServiceModel>();
		}

		public int GetAllUsersCount()
		{
			int result= this.context.Users.Count();

			return result;
		}
	}
}
