using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.InputModels.Answer;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
    public class AnswersController : AdminController
    {
		private readonly IAnswersService answersService;

		public AnswersController(IAnswersService answersService)
		{
			this.answersService = answersService;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return this.View();
		}

		
		[HttpPost(Name = "Create")]
		public async Task<IActionResult> Create(AnswerCreateInputModel answerCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(answerCreateInputModel);
			}

			AnswerServiceModel answerServiceModel = new AnswerServiceModel
			{
				Id = answerCreateInputModel.AuthorId,
				AuthorId = answerCreateInputModel.AuthorId,
				QuestionId = answerCreateInputModel.QuestionId
			};
			await this.answersService.CreateAnswer(answerServiceModel);
			return this.Redirect("/");
		}
	}
}