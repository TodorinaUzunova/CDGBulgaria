using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class CDGUserServiceModel:IdentityUser, IMapFrom<CDGUser>
	{
		public string FullName { get; set; }
	}
}
