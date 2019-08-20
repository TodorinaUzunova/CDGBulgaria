﻿using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
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
	public class ArticlesServiceTests
	{
		private ArticlesService articlesService;

		private IHttpContextAccessor contextAccessor;

		public ArticlesServiceTests()
		{
			MapperInitializer.InitializeMapper();
		}
		private List<Article> GetInitialData()
		{
			return new List<Article> {
				new Article {
					Title="CDGHealth",
					Content="Sofia is the place with a lot of specialized doctors for this diseases.",
					Author= new CDGUser{FullName="Petko Petkov" },
					
				  },
				new Article {
					Title="CDG ressources",
					Content="The are not so much written materials for this diseases.",
					Author= new CDGUser{FullName="Ivan Ivanov" }
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Articles.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllArticles_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "ArticlesService Method GetAllArticles() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			 this.articlesService = new ArticlesService(context, contextAccessor);


			List<ArticleServiceModel> actualResults = await this.articlesService.GetAllArticles().ToListAsync();

			List<ArticleServiceModel> expectedResults = GetInitialData().To<ArticleServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Title == actualEntry.Title, errorMessagePrefix + " " + "Title is not returned properly");
				Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + "Content is not returned properly");
				Assert.True(expectedEntry.Author.FullName== actualEntry.Author.FullName, errorMessagePrefix + " " + "AuthorFullName is not returned properly");
			}

		}

		[Fact]
		public async Task GetAllArticles_WithZeroData_ShouldReturnEmptyResult()
		{

			string errorMessagePrefix = "ArticlesService Method GetAllArticles() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();


			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			List<ArticleServiceModel> actualResults = await this.articlesService.GetAllArticles().ToListAsync();

			Assert.True(actualResults.Count == 0, errorMessagePrefix);

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "ArticlesService Method CreateArticle() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel articleServiceModel = new ArticleServiceModel()
			{
				Title = "CDGHealthMeeting",
				Content = "Sofia is the place, where the association is founded.",
				Author = new CDGUserServiceModel { FullName = "Ivan Petkov" }
			};

			bool actualResult = await this.articlesService.CreateArticle(articleServiceModel);

			Assert.True(actualResult, errorMessagePrefix);
		}


		[Fact]
		public async Task GetById_WithNonExistentId_ShouldReturnNull()
		{

			string errorMessagePrefix = "ArticlesService Method GetArticleById() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel actualResult = await this.articlesService.GetArticleById("fake1");

			Assert.True(actualResult == null, errorMessagePrefix);

		}

		[Fact]
		public async Task Edit_WithCorrectData_ShouldPassSuccesfully()
		{

			string errorMessagePrefix = "ArticlesService Method Edit() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel expectedData = context.Articles.First().To<ArticleServiceModel>();

			bool actualData = await this.articlesService.Edit(expectedData.Id, expectedData);

			Assert.True(actualData, errorMessagePrefix);

		}

		[Fact]
		public async Task Edit_WithCorrectData_ShouldEditArticleCorrectly()
		{

			string errorMessagePrefix = "ArticlesService Method Edit() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel expectedData = context.Articles.First().To<ArticleServiceModel>();

			expectedData.Title = "Edited Title";
			expectedData.Content = "Edited Content";
			expectedData.Author.FullName = "Petko Karavelov";

			await this.articlesService.Edit(expectedData.Id, expectedData);

			ArticleServiceModel actualData = context.Articles.First().To<ArticleServiceModel>();

			Assert.True(actualData.Title==expectedData.Title, errorMessagePrefix + "Title not edited properly");
			Assert.True(actualData.Content == expectedData.Content, errorMessagePrefix + "Content not edited properly");
			Assert.True(actualData.Author.FullName == expectedData.Author.FullName, errorMessagePrefix + "AuthorFullname not edited properly");
			
		}

		[Fact]
		public async Task Edit_WithGivenNonExistenId_ShouldThrowArgumentNullException()
		{

			string errorMessagePrefix = "ArticlesService Method Edit() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel expectedData = context.Articles.First().To<ArticleServiceModel>();

			expectedData.Title = "Edited Title";
			expectedData.Content = "Edited Content";
			expectedData.CreatedOn = DateTime.UtcNow;
			expectedData.AuthorId = "gfbehs";

			await this.articlesService.Edit(expectedData.Id, expectedData);

			await Assert.ThrowsAsync<ArgumentNullException>(() =>this.articlesService.Edit("Non-Existent", expectedData));
		}

		[Fact]
		public async Task Delete_WithCorrectData_ShouldPassSuccesfully()
		{

			string errorMessagePrefix = "ArticlesService Method Delete() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			ArticleServiceModel expectedData = context.Articles.First().To<ArticleServiceModel>();

			bool actualData = await this.articlesService.Delete(expectedData.Id);

			Assert.True(actualData, errorMessagePrefix);

		}


		[Fact]
		public async Task Delete_WithCorrectData_ShouldDeleteFromContext()
		{

			string errorMessagePrefix = "ArticlesService Method Delete() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			string idToDelete = context.Articles.First().To<ArticleServiceModel>().Id;

		    await this.articlesService.Delete(idToDelete);

			int expectedCount = 1;
			int actualCount = context.Articles.Count();

			Assert.True(expectedCount==actualCount, errorMessagePrefix);
		}

		[Fact]
		public async Task Delete_WithGivenNonExistenId_ShouldThrowArgumentNullException()
		{

			string errorMessagePrefix = "ArticlesService Method Delete() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.contextAccessor = new HttpContextAccessor();
			this.articlesService = new ArticlesService(context, contextAccessor);

			await Assert.ThrowsAsync<ArgumentNullException>(() => this.articlesService.Delete("Non-Existent"));
		}
	}
}
