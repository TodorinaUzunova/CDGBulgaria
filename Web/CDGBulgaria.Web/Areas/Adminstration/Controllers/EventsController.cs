using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels;
using CDGBulgaria.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Adminstration.Controllers
{
	
	public class EventsController : AdminController
	{
		private readonly IEventService eventService;
		private readonly ICloudinaryService cloudinaryService;

		public EventsController(IEventService eventService, ICloudinaryService cloudinaryService)
		{
			this.eventService = eventService;
			this.cloudinaryService = cloudinaryService;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return this.View();
		}

		[Area("Administration")]
		[Authorize(Roles ="Admin")]
		[HttpPost(Name = "Create")]
		public async Task<IActionResult> Create(EventCreateInputModel eventCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(eventCreateInputModel);
			}

			string fileUrl = await this.cloudinaryService.UploadFile(eventCreateInputModel.MoreInfo, eventCreateInputModel.Name);

			EventServiceModel eventServiceModel = new EventServiceModel()
			{
				Name = eventCreateInputModel.Name,
				Start=eventCreateInputModel.Start,
				Venue=eventCreateInputModel.Venue,
				MoreInfo=fileUrl

			};

			await this.eventService.Create(eventServiceModel);
			 return this.Redirect("/");
		}

		public async Task<IActionResult> All()
		{
			var events= this.eventService.GetAllEvents()
				.Select(ev=>new EventViewModel
				{
					Name=ev.Name,
					Start=ev.Start,
					Venue=ev.Venue,
					MoreInfo=ev.MoreInfo
				})
				.ToList();

			return this.View(events);
		}

	}
}