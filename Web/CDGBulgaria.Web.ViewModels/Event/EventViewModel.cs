using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Event
{
	public class EventViewModel
	{

		public string Name { get; set; }

		public string Venue { get; set; }

		public DateTime Start { get; set; }


		public string MoreInfo { get; set; }
	}
}
