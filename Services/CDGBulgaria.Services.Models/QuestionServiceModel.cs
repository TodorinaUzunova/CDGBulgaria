using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class QuestionServiceModel:IMapTo<Question>, IMapFrom<Question>
	{

		public string Id { get; set; }

		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public string AuthorId { get; set; }
		public CDGUserServiceModel Author { get; set; }

		public ICollection<AnswerServiceModel> Answers { get; set; }

	}
}
