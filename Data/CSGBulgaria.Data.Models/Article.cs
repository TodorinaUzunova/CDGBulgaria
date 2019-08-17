using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		[MaxLength(50)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		[NotMapped]
		public string Summary => this.Content.Substring(0, 100) +"...";

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public string  AuthorId { get; set; }
		public CDGUser Author { get; set; }
	}
}
