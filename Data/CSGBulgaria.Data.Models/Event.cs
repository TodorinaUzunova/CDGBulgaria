using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CDGBulgaria.Data.Models
{
	public class Event
	{
		public Event()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }

		public string Name { get; set; }



		public string Venue { get; set; }

		public DateTime Start { get; set; }

	
		public string MoreInfo { get; set; }


	}
}
