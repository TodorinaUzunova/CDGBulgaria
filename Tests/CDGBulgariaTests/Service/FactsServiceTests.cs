using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
using CDGBulgaria.Services.Contracts;
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
	public class FactsServiceTests
	{
		private IFactsService factsService;


		public FactsServiceTests()
		{
			MapperInitializer.InitializeMapper();
		}
		private List<Fact> GetInitialData()
		{
			return new List<Fact> {
				new Fact {
					Content="This is a new kind of rare desease.",
					PdfFile="src/pics/something.pdf"
				  },
				new Fact {
					Content="This desease appears mostly by children.",
					PdfFile="src/pics2/other.pdf"
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Facts.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllFacts_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "FactsService Method GetAllFacts() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.factsService = new FactsService(context);

		List<FactServiceModel> actualResults = await this.factsService.GetAllFacts().ToListAsync();

		List<FactServiceModel> expectedResults = GetInitialData().To<FactServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + "Content is not returned properly");
				Assert.True(expectedEntry.PdfFile == actualEntry.PdfFile, errorMessagePrefix + " " + "PdfFile is not returned properly");
			}

		}

		[Fact]
		public async Task GetAllFacts_WithZeroData_ShouldReturnEmptyResult()
		{

			string errorMessagePrefix = "FactsService Method GetAllFacts() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.factsService = new FactsService(context);

			List<FactServiceModel> actualResults = await this.factsService.GetAllFacts().ToListAsync();

			Assert.True(actualResults.Count == 0, errorMessagePrefix);

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "FactsService Method CreateEvent() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.factsService = new FactsService(context);

			FactServiceModel factus = new FactServiceModel()
			{
				Content="There are not yet found medicines for this desease.",
				PdfFile = "src/pics/something/sofia.pdf"
			};

			bool actualResult = await this.factsService.Create(factus);

			Assert.True(actualResult, errorMessagePrefix);

		}
	}
}
