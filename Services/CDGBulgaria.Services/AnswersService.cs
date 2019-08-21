using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels.Answer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services
{
	public class AnswersService : IAnswersService
	{
		private readonly CDGBulgariaDbContext context;
	

		public AnswersService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}
		public async Task<bool> CreateAnswer(AnswerServiceModel answerServiceModel)
		{
			Answer answer = new Answer
			{
				Content=answerServiceModel.Content,
				Question=new Question {Id=answerServiceModel.QuestionId,
					Content =answerServiceModel.Question.Content},
				AuthorId=answerServiceModel.AuthorId,
				
			};

			await this.context.Answers.AddAsync(answer);
			int result = await this.context.SaveChangesAsync();
			return result > 0;
		}

		public  IQueryable<AnswerServiceModel> GetAllAnswersForAQuestionById(string id)
		{
		     var answersForAQuestion = this.context.Answers.Include(a=>a.Question).Where(a=>a.QuestionId==id)
				.To<AnswerServiceModel>();

			return answersForAQuestion;
				 
		}
	}
}
