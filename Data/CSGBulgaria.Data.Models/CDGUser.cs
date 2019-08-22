using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class CDGUser : IdentityUser<string>
	{
		[Required]
		[MaxLength(80)]
		public string FullName { get; set; }	
	
	}
}
