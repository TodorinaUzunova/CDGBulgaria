using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CDGBulgaria.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CDGBulgaria.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<CDGUser> _signInManager;

        public LogoutModel(SignInManager<CDGUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult>  OnGetAsync()
        {
			await _signInManager.SignOutAsync();

			 return Redirect("/Identity/Account/Login");
		}

       
    }
}