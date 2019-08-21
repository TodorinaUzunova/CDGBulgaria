﻿using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgariaTests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CDGBulgariaTests.Service
{
	public class AnswersServiceTests
	{
		private AnswersService answersService;

		public AnswersServiceTests()
		{
			MapperInitializer.InitializeMapper();
		}
		private List<Answer> GetInitialData()
		{
			return new List<Answer> {
				new Answer {
					Content="Sofia is the place with a lot of specialized doctors for this diseases.",
					Question=new Question{ Content="How can be sure it is CDG?", Id="trtsjjsch567jscj"},
					Author= new CDGUser{UserName="Stancheva-Ivanova " },

				  },
				new Answer {
					Content="The are not so much written materials for this diseases.",
					Question=new Question{Content="Is there enough information about the disease?", Id="fgrt564fdf"},
					Author= new CDGUser{UserName="Ivan Ivanov" }
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Answers.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "AnswersService Method CreateAnswer() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);
			this.answersService = new AnswersService(context);

			AnswerServiceModel answerServiceModel = new AnswerServiceModel()
			{
				Content = "Sofia is the place, where the association is founded.",
				Question = new QuestionServiceModel { Id = "2er5dbvxzaaxax", Content = "Where is the association founded?" },
				Author = new CDGUserServiceModel { UserName = "Petkov" }
			};

			bool actualResult = await this.answersService.CreateAnswer(answerServiceModel);
			Assert.True(actualResult, errorMessagePrefix);
		}

		[Fact]
	  public async Task GetAllAnswersForAQuestionById_WithCorrectData_ShouldReturnCorrectResult()
		{
			string errorMessagePrefix = "AnswersService Method GetAllAnswersForQuestionById() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);
			this.answersService = new AnswersService(context);

			List<AnswerServiceModel> expectedResults = GetInitialData().Where(a => a.QuestionId == "trtsjjsch567jscj").To<AnswerServiceModel>().ToList();
			List<AnswerServiceModel> actualResults = this.answersService.GetAllAnswersForAQuestionById("trtsjjsch567jscj")
				.To<AnswerServiceModel>()
				.ToList();

			Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix);

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + " Content is not returned properly");
				Assert.True(expectedEntry.QuestionId == actualEntry.QuestionId, errorMessagePrefix + " " + "QuestionId is not returned properly");
				Assert.True(expectedEntry.Author.UserName == actualEntry.Author.UserName, errorMessagePrefix + " " + "AuthorUsername is not returned properly");
			}
		}

			[Fact]
		public async Task GetAllAnswersForAQuestionById_WithNonExistentQuestionId_ShouldReturnZeroCount()
		{
			string errorMessagePrefix = "AnswersService Method GetAllAnswersForAQuestionById() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);
			this.answersService = new AnswersService(context);

			List<AnswerServiceModel> actualResult = await this.answersService.GetAllAnswersForAQuestionById("fake1").ToListAsync();

			Assert.True(actualResult.Count == 0, errorMessagePrefix);

		}
	}
}

