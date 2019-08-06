using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels.Event;
using CDGBulgaria.Web.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Adminstration.Controllers
{
	
	public class EventsController : AdminController
	{
		private readonly IEventsService eventsService;
		private readonly ICloudinaryService cloudinaryService;

		public EventsController(IEventsService eventsService, ICloudinaryService cloudinaryService)
		{
			this.eventsService = eventsService;
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

			await this.eventsService.Create(eventServiceModel);
			 return this.Redirect("/");
		}

		
	}
}