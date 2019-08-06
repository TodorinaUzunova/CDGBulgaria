using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Article
{
	public class ArticleDeleteViewModel : IMapFrom<ArticleServiceModel>
	{

		public string Title { get; set; }

		public string Content { get; set; }

		public string AuthorId { get; set; }

		public string AuthorFullName { get; set; }

	}
}
