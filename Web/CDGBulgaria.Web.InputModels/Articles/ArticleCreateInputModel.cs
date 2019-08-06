using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Articles
{
	public class ArticleCreateInputModel : IMapTo<ArticleServiceModel>, IHaveCustomMappings
	{
		[Required(ErrorMessage = "Title is required!")]
		[StringLength(50)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Content is required!")]
		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public string AuthorId { get; set; }

		[Required]
		public string AuthorFullName { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<ArticleCreateInputModel, ArticleServiceModel>()
					.ForMember(destination => destination.Author,
							   opts => opts.MapFrom(origin => new CDGUserServiceModel() { FullName = origin.AuthorFullName }));
		}
	}
}
