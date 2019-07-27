﻿using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CDGBulgaria.Web.InputModels
{
	public class EventCreateInputModel:IMapTo<EventServiceModel>//IHaveCustomMappings
	{
		[Required]
		[Display(Name = "Username")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Venue")]
		public string Venue { get; set; }

		[Required]
		[Display(Name = "Start")]
		public DateTime Start { get; set; }

		[Required]
		[Display(Name = "MoreInfo")]
		public IFormFile MoreInfo { get; set; }

		//public void CreateMappings(IProfileExpression configuration)
		//{
		//	configuration.CreateMap<ProductCreateInputModel, ProductServiceModel>()
		//		.ForMember(destination => destination.ProductType,
		//		opts => opts.MapFrom(origin => new ProductTypeServiceModel { Name = origin.ProductType });	
		//}
	}
}
