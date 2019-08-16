using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
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
		private readonly IHttpContextAccessor contextAccessor;

		public AnswersService(CDGBulgariaDbContext context, IHttpContextAccessor contextAccessor)
		{
			this.context = context;
			this.contextAccessor = contextAccessor;
		}
		public async Task<bool> CreateAnswer(AnswerServiceModel answerServiceModel)
		{
			string authorId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			Question questionFromDb = context.Questions.SingleOrDefault(question=>question.Content==answerServiceModel.Question.Content); 

			Answer answer = new Answer
			{
				Id = answerServiceModel.Id,
				Content=answerServiceModel.Content,
				AuthorId = authorId,
				QuestionId=questionFromDb.Id,
				Question = questionFromDb,
			};

			await this.context.Answers.AddAsync(answer);
			int result = await this.context.SaveChangesAsync();
			return result > 0;
		}

		public IQueryable<AnswerServiceModel> GetAllAnswersForAQuestionById(string id)
		{
		     var answersForAQuestion = this.context.Answers.Where(a=>a.QuestionId==id)
				.To<AnswerServiceModel>();

			return answersForAQuestion;
				 
		}
	}
}
