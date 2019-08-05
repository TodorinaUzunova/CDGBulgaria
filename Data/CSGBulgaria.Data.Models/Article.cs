using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class Article
	{
		public Article()
		{
			this.Id = Guid.NewGuid().ToString(); 
		}
		[Key]
		public string Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		[MaxLength(800)]
		public string Content { get; set; }
		 
		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public string  AuthorId { get; set; }
		public CDGUser Author { get; set; }
	}
}
