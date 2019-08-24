using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.ViewModels.Answer;
using CDGBulgaria.Web.ViewModels.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
	public class QuestionsController : AdminController
	{
		private readonly IQuestionsService questionsService;
		private readonly IAnswersService answersService;

		public QuestionsController(IQuestionsService questionsService, IAnswersService answersService)
		{
			this.questionsService = questionsService;
			this.answersService = answersService;
		}

		//[HttpGet(Name = "Delete")]
		//public async Task<IActionResult> Delete(string id)
		//{
		//	if (id == null)
		//	{
		//		return NotFound();
		//	}

		//	QuestionDeleteViewModel questionDeleteViewModel = (await this.questionsService.GetQuestionById(id)).To<QuestionDeleteViewModel>();

		//	if (questionDeleteViewModel == null)
		//	{
		//		return this.Redirect("/");
		//		throw new ArgumentNullException(nameof(questionDeleteViewModel));
		//	}

		//	return this.View(questionDeleteViewModel);
		//}

		//[HttpPost]
		//[Route("/Administration/Questions/Delete/{id}")]
		//public async Task<IActionResult> DeleteConfirm(string id)
		//{

		//	await this.questionsService.Delete(id);
		//	return this.Redirect("/Questions/All");
		//}
	}
}