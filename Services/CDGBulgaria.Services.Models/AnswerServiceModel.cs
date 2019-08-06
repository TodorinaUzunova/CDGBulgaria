using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class AnswerServiceModel:IMapTo<Answer>, IMapFrom<Answer>
	{
		public string Id { get; set; }

		public string Content { get; set; }

		public string AuthorId { get; set; }
		public CDGUserServiceModel Author { get; set; }


		public string QuestionId { get; set; }
		public QuestionServiceModel Question { get; set; }
	}
}
