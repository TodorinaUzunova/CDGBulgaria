using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class Question
	{

		public int Id { get; set; }

		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public string AuthorId { get; set; }
		public CDGUser Author { get; set; }

		public ICollection<Reply> Replies { get; set; }



	}
}

