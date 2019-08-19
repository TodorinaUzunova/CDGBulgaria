using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Articles
{
	public class ArticleEditInputModel:IMapTo<ArticleServiceModel>, IMapFrom<ArticleServiceModel>, IHaveCustomMappings
	{

		[Required(ErrorMessage = "Title is required!")]
		[StringLength(50)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Content is required!")]
		public string Content { get; set; }

		[Required]
		public string AuthorId { get; set; }

		[Required(ErrorMessage = "FullName is required!")]
		public string AuthorFullName { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<ArticleServiceModel, ArticleEditInputModel>()
				.ForMember(destination => destination.AuthorFullName,
				opts => opts.MapFrom(origin => origin.Author.FullName));


			configuration.CreateMap<ArticleEditInputModel, ArticleServiceModel>()
					.ForMember(destination => destination.Author,
							   opts => opts.MapFrom(origin => new CDGUserServiceModel() { FullName = origin.AuthorFullName }));
		}
	}
}
