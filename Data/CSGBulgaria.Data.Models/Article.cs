using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class Article
	{
		public Article()
		{
			this.Id = Guid.NewGuid().ToString(); 
		}
		public string Id { get; set; }

		public string Content { get; set; }
		 
		public DateTime CreatedOn { get; set; }

		public string  AuthorId { get; set; }
		public CDGUser Author { get; set; }
	}
}
