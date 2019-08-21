using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Web.ViewModels.Answer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
    public class AnswersController : Controller
    {
		private readonly IAnswersService answersService;

		public AnswersController(IAnswersService answersService)
		{
			this.answersService = answersService;
		}

	    [HttpGet]
		[Route("/Answers/All")]
        public async Task<IActionResult> All([FromQuery]string id)
        {
			if (id == null)
			{
				return NotFound();
			}
			var allAnswers = await this.answersService.GetAllAnswersForAQuestionById(id)
				.Select(a=>new AnswerViewModel
				{ QuestionId=a.QuestionId,
				QuestionContent=a.Question.Content,
				Content=a.Content})
				.ToListAsync();

			
            return this.View(allAnswers);
        }
    }
}