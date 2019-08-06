using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class ArticleServiceModel: IMapTo<Article>, IMapFrom<Article>
	{
		public string Id { get; set; }

	
		public string Title { get; set; }

		
		public string Content { get; set; }

		
		public DateTime CreatedOn { get; set; }

		public string AuthorId { get; set; }

		public CDGUserServiceModel Author { get; set; }

	}
}
