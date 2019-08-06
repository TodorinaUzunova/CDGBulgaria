using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.InputModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
	public class DoctorsController : AdminController
	{
		private readonly UserManager<CDGUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DoctorsController(UserManager<CDGUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}


		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return this.View();
		}

		[HttpPost(Name = "Create")]
		public async Task<IActionResult> CreateConfirm(DoctorCreateInputModel doctorCreateInputModel)
		{
			if (ModelState.IsValid)
			{

				var user = new CDGUser { UserName = doctorCreateInputModel.Username, Email = doctorCreateInputModel.Email };
				var result = await _userManager.CreateAsync(user, doctorCreateInputModel.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "Doctor");

					return Redirect("/");
				}


				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}


			return this.View(doctorCreateInputModel);
		}

	}
}