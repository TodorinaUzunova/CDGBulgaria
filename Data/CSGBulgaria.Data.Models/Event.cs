using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[Key]
		public string Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string Name { get; set; }


		[Required]
		[MaxLength(200)]
		public string Venue { get; set; }

		[Required]
		public DateTime Start { get; set; }

	    [Required]
		public string MoreInfo { get; set; }


	}
}
