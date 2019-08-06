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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CDGBulgariaTests.Service
{
	public class QuestionsServiceTests
	{
		private IQuestionsService questionsService;
		private IHttpContextAccessor contextAccessor;

		public QuestionsServiceTests()
		{
			MapperInitializer.InitializeMapper();

		}

		private List<Question> GetInitialData()
		{
			return new List<Question> {
				new Question {
					Content="Is this is a new kind of rare desease?",
					AuthorId="cvthsg12lkmm",
					CreatedOn=DateTime.UtcNow
				  },
				new Question {
					Content="What happens, when the children have this desease?",
					AuthorId="cvthsg12lkmm3456",
					CreatedOn=DateTime.UtcNow
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

			this.questionsService = new QuestionsService(context, contextAccessor);

			List<QuestionServiceModel> actualResults = await this.questionsService.GetAllQuestions().ToListAsync();

			List<QuestionServiceModel> expectedResults = GetInitialData().To<QuestionServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + "Content is not returned properly");
				Assert.True(expectedEntry.CreatedOn == actualEntry.CreatedOn, errorMessagePrefix + " " + "CreatedOn is not returned properly");
				Assert.True(expectedEntry.AuthorId == actualEntry.AuthorId, errorMessagePrefix + " " + "AuthorId is not returned properly");
			}

		}

		[Fact]
		public async Task GetAllQuestions_WithZeroData_ShouldReturnEmptyResult()
		{

			string errorMessagePrefix = "QuestionsService Method GetAllQuestions() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.questionsService = new QuestionsService(context, contextAccessor);

			List<QuestionServiceModel> actualResults = await this.questionsService.GetAllQuestions().ToListAsync();

			Assert.True(actualResults.Count == 0, errorMessagePrefix);

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "QuestionsService Method CreateQuestion() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();
			this.contextAccessor = new HttpContextAccessor();

			this.questionsService = new QuestionsService(context, contextAccessor);

		 QuestionServiceModel questio= new QuestionServiceModel()
			{
				Content = "Are there really found medicines for this desease?",
				AuthorId="sdwrefghjk12345",
			};

			bool actualResult = await this.questionsService.Create(questio);

			Assert.True(actualResult, errorMessagePrefix);

		}
	}
}

