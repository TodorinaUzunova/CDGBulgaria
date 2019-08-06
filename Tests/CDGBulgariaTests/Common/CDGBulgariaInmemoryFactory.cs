using CDGBulgaria.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgariaTests.Common
{
	public static class CDGBulgariaInmemoryFactory
	{

		public static CDGBulgariaDbContext InitializeContext()
		{

			var options = new DbContextOptionsBuilder<CDGBulgariaDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var context = new CDGBulgariaDbContext(options);

			return context;
		}
	}
}
