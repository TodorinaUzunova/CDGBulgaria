using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services
{
	public class EventsService : IEventsService
	{
		public readonly CDGBulgariaDbContext context;

		public EventsService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> Create(EventServiceModel eventServiceModel)
		{
			Event eve=new Event()
			{
				Name=eventServiceModel.Name,
				Start=eventServiceModel.Start,
			     Venue=eventServiceModel.Venue,
				 MoreInfo=eventServiceModel.MoreInfo

			};

		     await context.Events.AddAsync(eve);
			 int result = await context.SaveChangesAsync();

			return result >0;
		}

		public IQueryable<EventServiceModel> GetAllEvents()
		{
			var allEvents = this.context.Events
				 .Select(ev => new EventServiceModel
				 {
					 Name=ev.Name,
					 Start=ev.Start,
					 Venue=ev.Venue,
					 MoreInfo=ev.MoreInfo
				 });

			return allEvents;
		}
	}
}
