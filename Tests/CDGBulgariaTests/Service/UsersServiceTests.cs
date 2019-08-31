using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgariaTests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CDGBulgariaTests.Service
{
	public class UsersServiceTests
	{

		private IUsersService usersService;


		public UsersServiceTests()
		{
			MapperInitializer.InitializeMapper();
		}

		private List<CDGUser> GetInitialData()
		{
			return new List<CDGUser> {
				new CDGUser {
					FullName="Atanas Atanasov",
				  },
				new CDGUser {
					FullName="Dobrin Dobrinov"
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Users.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllUsersCount_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "UsersService Method GetAllUsersCount() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.usersService = new UsersService(context);

			int actualResults = this.usersService.GetAllUsersCount();

			int expectedResults = GetInitialData().Count();

			Assert.True(expectedResults==actualResults);	
		}

		[Fact]
		public async Task GetAllUsers_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "UsersService Method GetAllUsers() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.usersService = new UsersService(context);

			List<CDGUserServiceModel> actualResults = await this.usersService.GetAllUsersByFullName().ToListAsync();

			List<CDGUserServiceModel> expectedResults = GetInitialData().To<CDGUserServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.FullName== actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly");
				Assert.True(expectedEntry.UserName == actualEntry.UserName, errorMessagePrefix + " " + "UserName is not returned properly");
			}

		}
	}
}
