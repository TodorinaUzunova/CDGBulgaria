using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CDGBulgaria.Web.Models;

namespace CDGBulgaria.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return this.View();
		}

		public IActionResult About()
		{
			return this.View();
		}

		public IActionResult Help()
		{
			return this.View();
		}


	}
}
