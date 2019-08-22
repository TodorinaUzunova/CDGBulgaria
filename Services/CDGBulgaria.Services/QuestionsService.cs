using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
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
	public class QuestionsService:IQuestionsService
	{
		private readonly CDGBulgariaDbContext context;

		public QuestionsService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> Create(QuestionServiceModel questionServiceModel)
		{
		
			Question question = new Question
			{
				Content = questionServiceModel.Content,
				CreatedOn=questionServiceModel.CreatedOn,
				AuthorId=questionServiceModel.AuthorId,
			}; 

			await this.context.Questions.AddAsync(question);
			int result = await this.context.SaveChangesAsync();
			return result > 0;
		}

		public IQueryable<QuestionServiceModel> GetAllQuestions()
		{
			
			var  allQuestions=this.context.Questions.To<QuestionServiceModel>();

			return allQuestions;
		}

		public async Task<QuestionServiceModel> GetQuestionById(string id)
		{
			QuestionServiceModel question = await this.context.Questions.To<QuestionServiceModel>()
				.SingleOrDefaultAsync(a => a.Id == id);

			return question;
		}

		public async Task<bool> Delete(string id)
		{
			Question questionFromDb = await this.context.Questions.SingleOrDefaultAsync(a => a.Id == id);

			if (questionFromDb == null)
			{
				throw new ArgumentNullException(nameof(questionFromDb));
			}

			this.context.Questions.Remove(questionFromDb);
			int result = await this.context.SaveChangesAsync();

			return result > 0;
		}
	}
}
