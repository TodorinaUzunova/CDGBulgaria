using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Doctor
{
	public class DoctorCreateInputModel
	{

		[Required]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}
