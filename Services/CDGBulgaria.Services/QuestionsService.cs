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
	public class QuestionsService:IQuestionsService
	{
		private readonly CDGBulgariaDbContext context;
		private readonly IHttpContextAccessor contextAccessor;


		public QuestionsService(CDGBulgariaDbContext context, IHttpContextAccessor contextAccessor)
		{
			this.context = context;
			this.contextAccessor = contextAccessor;
		}

		public async Task<bool> Create(QuestionServiceModel questionServiceModel)
		{
			string authorId= contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			Question question = new Question
			{
				Content = questionServiceModel.Content,
				AuthorId= authorId,
				CreatedOn = questionServiceModel.CreatedOn,
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
	}
}
