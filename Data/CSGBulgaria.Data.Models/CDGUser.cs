using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class CDGUser : IdentityUser<string>
	{

		public CDGUser()
		{
		
		}
		public string FullName { get; set; }	
	
	}
}
