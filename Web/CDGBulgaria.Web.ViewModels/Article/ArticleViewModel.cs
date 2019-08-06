using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Article
{
	public class ArticleViewModel:IMapFrom<ArticleServiceModel>, IHaveCustomMappings
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public string AuthorFullName { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<ArticleServiceModel, ArticleViewModel>()
				.ForMember(destination => destination.AuthorFullName,
				opts => opts.MapFrom(origin => origin.Author.FullName));
		}
	}
}
