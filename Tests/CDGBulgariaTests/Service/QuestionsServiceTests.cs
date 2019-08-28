using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgariaTests.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CDGBulgariaTests.Service
{
	public class QuestionsServiceTests
	{
		private IQuestionsService questionsService;
	
		public QuestionsServiceTests()
		{
			MapperInitializer.InitializeMapper();

		}

		private List<Question> GetInitialData()
		{
			return new List<Question> {
				new Question {
					Content="Is this is a new kind of rare desease?",
					
				  },
				new Question {
					Content="What happens, when the children have this desease?",
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Questions.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllQuestions_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "QuestionsService Method GetAllQuestions() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);
			this.questionsService = new QuestionsService(context);

			List<QuestionServiceModel> actualResults = await this.questionsService.GetAllQuestions().ToListAsync();

			List<QuestionServiceModel> expectedResults = GetInitialData().To<QuestionServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + "Content is not returned properly");
				Assert.True(expectedEntry.Author.UserName == actualEntry.Author.UserName, errorMessagePrefix + " " + "AuthorUsername is not returned properly");
			}

		}

		[Fact]
		public async Task GetAllQuestions_WithZeroData_ShouldReturnEmptyResult()
		{

			string errorMessagePrefix = "QuestionsService Method GetAllQuestions() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();
			this.questionsService = new QuestionsService(context);

			List<QuestionServiceModel> actualResults = await this.questionsService.GetAllQuestions().ToListAsync();

			Assert.True(actualResults.Count == 0, errorMessagePrefix);

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "QuestionsService Method CreateQuestion() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();
			this.questionsService = new QuestionsService(context);

		 QuestionServiceModel questionModel= new QuestionServiceModel()
			{
				Content = "Are there really found medicines for this desease?",
				AuthorId= "trahjgtss123",
				CreatedOn= DateTime.Parse("10/07/2019 10:30"),
			};
			
			bool actualResult = await this.questionsService.Create(questionModel);

			Assert.True(actualResult, errorMessagePrefix);

		}
	}
}

