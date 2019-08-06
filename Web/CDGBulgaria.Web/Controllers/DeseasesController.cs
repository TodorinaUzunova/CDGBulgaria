using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Controllers
{
    public class DeseasesController : Controller
    {
        public async Task< IActionResult> About()
        {
            return View();
        }

		public async Task<IActionResult> Info()
		{
			return View();
		}
		public async Task<IActionResult> Details()
		{
			return View();
		}
	}
}