using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Articles
{
	public class ArticleDeleteInputModel : IMapFrom<ArticleServiceModel>
	{

		public string Title { get; set; }

		public string Content { get; set; }

		public string AuthorFullName { get; set; }

	}
}
