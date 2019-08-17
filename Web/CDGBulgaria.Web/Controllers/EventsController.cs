using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
    public class EventsController : Controller
    {
		private readonly IEventsService eventService;

		public EventsController(IEventsService eventService)
		{
			this.eventService = eventService;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> All()
		{
			var events = await this.eventService.GetAllEvents()
				.Select(ev => new EventViewModel
				{
					Name = ev.Name,
					Start = ev.Start,
					Venue = ev.Venue,
					MoreInfo = ev.MoreInfo
				})
				.ToListAsync();

			return this.View(events);
		}

	}
}