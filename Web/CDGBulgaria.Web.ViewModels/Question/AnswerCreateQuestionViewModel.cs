using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Question
{
	public class AnswerCreateQuestionViewModel:IMapFrom<QuestionServiceModel>
	{
		public string Id { get; set; } 

		public string  Content  { get; set; }
	}
}
