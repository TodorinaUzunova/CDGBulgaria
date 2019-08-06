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
		public readonly CDGBulgariaDbContext dbContext;

		public EventsService(CDGBulgariaDbContext dbContext)
		{
			this.dbContext = dbContext;
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

		     await dbContext.Events.AddAsync(eve);
			 int result = await dbContext.SaveChangesAsync();

			return result >0;
		}

		public IQueryable<EventServiceModel> GetAllEvents()
		{
			var allEvents = this.dbContext.Events
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
