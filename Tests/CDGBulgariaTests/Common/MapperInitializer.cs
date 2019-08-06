using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CDGBulgariaTests.Common
{
	public static class MapperInitializer
	{
		public static void InitializeMapper()
		{

			 AutoMapperConfig.RegisterMappings(
				typeof(EventServiceModel).GetTypeInfo().Assembly,
					typeof(Event).GetTypeInfo().Assembly
				);

		}
	}
}
