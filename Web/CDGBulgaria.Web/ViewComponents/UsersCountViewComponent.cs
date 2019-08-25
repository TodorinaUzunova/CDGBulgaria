using CDGBulgaria.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDGBulgaria.Web.ViewComponents
{
	public class UsersCountViewComponent:ViewComponent
	{
		private readonly IUsersService usersService;

		public UsersCountViewComponent(IUsersService usersService)
		{
			this.usersService = usersService;
		}

		public IViewComponentResult Invoke()
		{
			return this.View(new UsersCountViewComponentViewModel { UsersCount = this.usersService.GetAllUsersCount() });
		}
	}

	public class UsersCountViewComponentViewModel
	{
		public int  UsersCount { get; set; }
	}
}

