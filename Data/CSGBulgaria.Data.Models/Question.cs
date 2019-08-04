using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class Question
	{
		public Question()
		{
		this.Id =  Guid.NewGuid().ToString();
	     }

		[Required]
		public string Id { get; set; }

	    [Required]
		public string Content { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }

		public string AuthorId { get; set; }
		public CDGUser Author { get; set; }

		public ICollection<Answer> Answers { get; set; }



	}
}

