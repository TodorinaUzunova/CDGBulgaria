using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.ViewModels.Answer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Question
{
	public class QuestionDeleteViewModel:IMapFrom<QuestionServiceModel>
	{
	
		public string Content { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string AuthorId { get; set; }

		public string AuthorUserName { get; set; }

	}
}
