using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Question
{
	public class QuestionCreateInputModel:IMapTo<QuestionServiceModel>
	{

		[Required(ErrorMessage = "Content is required!")]
		[Display(Name ="Content")]
		public string Content { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string AuthorId { get; set; }

		public string AuthorUserName { get; set; }
	}
}
