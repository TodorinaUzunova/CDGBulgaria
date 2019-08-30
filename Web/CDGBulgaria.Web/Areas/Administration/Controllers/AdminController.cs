using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Adminstration.Controllers
{
	[Area("Administration")]
	[Authorize(Roles = "Admin")]
	[AutoValidateAntiforgeryToken]
	public abstract class AdminController : Controller
    {
    }
}