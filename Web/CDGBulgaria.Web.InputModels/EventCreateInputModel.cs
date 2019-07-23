using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace CDGBulgaria.Web.InputModels
{
	public class EventCreateInputModel:IMapTo<EventServiceModel>//IHaveCustomMappings
	{
		public string Name { get; set; }

		public string Venue { get; set; }

		public DateTime Start { get; set; }


		public IFormFile MoreInfo { get; set; }

		//public void CreateMappings(IProfileExpression configuration)
		//{
		//	configuration.CreateMap<ProductCreateInputModel, ProductServiceModel>()
		//		.ForMember(destination => destination.ProductType,
		//		opts => opts.MapFrom(origin => new ProductTypeServiceModel { Name = origin.ProductType });	
		//}
	}
}
