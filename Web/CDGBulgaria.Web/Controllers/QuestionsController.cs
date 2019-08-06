﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels.Question;
using CDGBulgaria.Web.ViewModels.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Controllers
{
    public class QuestionsController : Controller
    {
		private readonly IQuestionsService questionService;

		public QuestionsController(IQuestionsService questionService)
		{
			this.questionService = questionService;
		}

		public async Task<IActionResult> Create()
		{


			//return this.View();

			//QuestionServiceModel serviceModel = model.To<QuestionServiceModel>();

	//	await this.questionService.Create(serviceModel);
			return this.View();

		}

		[HttpPost(Name ="Create")]
		[Authorize]
		public async Task<IActionResult> Create(QuestionCreateInputModel model)
        {


			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			
			}

			QuestionServiceModel serviceModel = model.To<QuestionServiceModel>();
			await this.questionService.Create(serviceModel);

			return this.Redirect("/");
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var questions = this.questionService.GetAllQuestions()
				.Select(question => new QuestionViewModel
				{
					Content=question.Content,
					CreatedOn=question.CreatedOn,
					AuthorUserName=question.Author.UserName
				})
				.ToList();

			return this.View(questions);
		}
	}
}