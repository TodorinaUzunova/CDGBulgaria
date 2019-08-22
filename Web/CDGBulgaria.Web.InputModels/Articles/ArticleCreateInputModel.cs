using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Articles
{
	public class ArticleCreateInputModel : IMapTo<ArticleServiceModel>
	{
		[Required(ErrorMessage = "Title is required!")]
		[StringLength(80)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Content is required!")]
		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }
	
	}
}
