using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CDGBulgaria.Web.InputModels.Event
{
	public class EventCreateInputModel:IMapTo<EventServiceModel>
	{
		[Required(ErrorMessage = "Name is required!")]
		[Display(Name = "Username")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Venue is required!")]
		[Display(Name = "Venue")]
		public string Venue { get; set; }

		[Required(ErrorMessage = "Start is required!")]
		[Display(Name = "Start")]
		public DateTime Start { get; set; }

		[Required(ErrorMessage = "MoreInfo is required!")]
		[Display(Name = "MoreInfo")]
		public IFormFile MoreInfo { get; set; }

	}
}
