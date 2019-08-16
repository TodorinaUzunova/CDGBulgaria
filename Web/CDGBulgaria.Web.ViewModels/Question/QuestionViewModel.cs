using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Question
{
	public class QuestionViewModel:IMapFrom<QuestionServiceModel>, IHaveCustomMappings
	{

		public string Id { get; set; }

		public string Content { get; set; }

		public DateTime? CreatedOn{ get; set; }

		public string AuthorUserName { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<QuestionServiceModel, QuestionViewModel>()
				.ForMember(destination=>destination.AuthorUserName,
				opts=>opts.MapFrom(origin=>origin.Author.UserName));
		}
	}
}
