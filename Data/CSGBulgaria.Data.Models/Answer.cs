using System;
using System.ComponentModel.DataAnnotations;

namespace CDGBulgaria.Data.Models
{
	public class Answer
	{

		public Answer()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		[Key]
		public string Id { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public string AuthorId { get; set; }
		public CDGUser Author { get; set; }

	
		public string QuestionId { get; set; }
		[Required]
		public Question Question { get; set; }

	}
}