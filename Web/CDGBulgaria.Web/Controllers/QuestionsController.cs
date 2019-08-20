﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels.Question;
using CDGBulgaria.Web.ViewModels.Answer;
using CDGBulgaria.Web.ViewModels.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
    public class QuestionsController : Controller
    {
		private readonly IQuestionsService questionsService;
		

		public QuestionsController(IQuestionsService questionsService)
		{
			this.questionsService = questionsService;
		}

		public async Task<IActionResult> Create()
		{
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
			model.CreatedOn = DateTime.UtcNow;
			QuestionServiceModel serviceModel = model.To<QuestionServiceModel>();
			await this.questionsService.Create(serviceModel);

			return this.Redirect("/Questions/All");
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var questions = await this.questionsService.GetAllQuestions()
				.Select(question => new QuestionViewModel
				{
					Content=question.Content,
					CreatedOn=question.CreatedOn,
					AuthorUserName=question.Author.UserName,
					//Answers=question.Answers.To<AnswerViewModel>().ToList(),
				})
				.ToListAsync();

			return this.View(questions);
		}
	}
}