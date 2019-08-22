using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CDGBulgaria.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CDGBulgaria.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CDGUser> _signInManager;
        private readonly UserManager<CDGUser> _userManager;
		private readonly RoleManager<IdentityRole>_roleManager;


        public RegisterModel(
            SignInManager<CDGUser> signInManager,
			UserManager<CDGUser> userManager,
		    RoleManager<IdentityRole> roleManager
		)
        {
		     
		    _userManager = userManager;
            _signInManager = signInManager;
			_roleManager=roleManager;
		
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
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

			[Required]
			[Display(Name = "FullName")]
			public string FullName  { get; set; }
		}

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = "/Identity/Account/Login";
            if (ModelState.IsValid)
            {
				var isRoot = !_userManager.Users.Any();
                var user = new CDGUser { UserName = Input.Username, Email = Input.Email, FullName=Input.FullName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

					if (isRoot)
					{
						await _userManager.AddToRoleAsync(user, "Admin");
					}
					else
					{
						await _userManager.AddToRoleAsync(user, "User");
					}
					
					return Redirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
