using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Answer
{
	public class AnswerViewModel : IMapFrom<AnswerServiceModel>, IHaveCustomMappings
	{
		public string Id  { get; set; }

		public string QuestionId { get; set; }

		public string QuestionContent { get; set; }

		public string Content { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<AnswerServiceModel, AnswerViewModel>()
				.ForMember(destination=>destination.QuestionContent,
				opts=>opts.MapFrom(origin=>origin.Question.Content));
		}
	}
}
