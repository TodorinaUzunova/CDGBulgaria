using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class EventServiceModel:IMapTo<Event>, IMapFrom<Event>
	{
		

		public string Name { get; set; }
			   
		public string Venue { get; set; }

		public DateTime Start { get; set; }


		public string MoreInfo { get; set; }
	}
}
