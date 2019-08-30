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
	public class DiseasesServiceTests
	{
		private DiseasesService diseasesService;


		public DiseasesServiceTests()
		{
			MapperInitializer.InitializeMapper();
		}
		private List<CDGDisease> GetInitialData()
		{
			return new List<CDGDisease> {
				new CDGDisease {
					Name=" MPMM2",
					Description="Very dangerous disease",
				  },
				new CDGDisease {
					Name=" MPMD2",
					Description="A new kind of disease",
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.CDGDiseases.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllCDGDiseases_WithInitialData_ShouldReturnCorrectResult()
		{

			string errorMessagePrefix = "DiseasesService Method GetAll() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);


			this.diseasesService = new DiseasesService(context);


			List<CDGDiseaseServiceModel> actualResults = await this.diseasesService.GetAll().ToListAsync();

			List<CDGDiseaseServiceModel> expectedResults = GetInitialData().To<CDGDiseaseServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly");
				Assert.True(expectedEntry.Description == actualEntry.Description, errorMessagePrefix + " " + "Description is not returned properly");
			}

		}

		[Fact]
		public async Task GetAllDiseases_WithZeroData_ShouldReturnEmptyResult()
		{

			string errorMessagePrefix = "DiseasesService Method GetAll() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.diseasesService = new DiseasesService(context);

			List<CDGDiseaseServiceModel> actualResults = await this.diseasesService.GetAll().ToListAsync();

			Assert.True(actualResults.Count == 0, errorMessagePrefix);

		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "DiseasesService Method CreateDisease() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel diseaseServiceModel = new CDGDiseaseServiceModel()
			{
				Name = "PMM3",
				Description = "The next disease from this row of diseases",
			};

			bool actualResult = await this.diseasesService.CreateDisease(diseaseServiceModel);

			Assert.True(actualResult, errorMessagePrefix);
		}


		[Fact]
		public async Task GetById_WithNonExistentId_ShouldReturnNull()
		{

			string errorMessagePrefix = "DiseaseService Method GetArticleById() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel actualResult = await this.diseasesService.GetCDGDiseaseById(1000);

			Assert.True(actualResult == null, errorMessagePrefix);

		}

		[Fact]
		public async Task Edit_WithCorrectData_ShouldPassSuccesfully()
		{

			string errorMessagePrefix = "DiseasesService Method Edit() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel expectedData = context.CDGDiseases.First().To<CDGDiseaseServiceModel>();

			bool actualData = await this.diseasesService.Edit(expectedData.Id, expectedData);

			Assert.True(actualData, errorMessagePrefix);

		}

		[Fact]
		public async Task Edit_WithCorrectData_ShouldEditCDGDiseaseCorrectly()
		{

			string errorMessagePrefix = "DiseasesService Method Edit() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel expectedData = context.CDGDiseases.First().To<CDGDiseaseServiceModel>();

			expectedData.Name = "Edited Name";
			expectedData.Description = "Edited Description";

			await this.diseasesService.Edit(expectedData.Id, expectedData);

			CDGDiseaseServiceModel actualData = context.CDGDiseases.First().To<CDGDiseaseServiceModel>();

			Assert.True(actualData.Name == expectedData.Name, errorMessagePrefix + "Name not edited properly");
			Assert.True(actualData.Description == expectedData.Description, errorMessagePrefix + "Description not edited properly");
		}

		[Fact]
		public async Task Edit_WithGivenNonExistenId_ShouldThrowArgumentNullException()
		{

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);
			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel expectedData = context.CDGDiseases.First().To<CDGDiseaseServiceModel>();

			expectedData.Name = "Edited Name";
			expectedData.Description = "Edited Description";

			await this.diseasesService.Edit(expectedData.Id, expectedData);

			await Assert.ThrowsAsync<ArgumentNullException>(() => this.diseasesService.Edit(1000, expectedData));
		}

		[Fact]
		public async Task Delete_WithCorrectData_ShouldPassSuccessfully()
		{

			string errorMessagePrefix = "DiseasesService Method Delete() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			CDGDiseaseServiceModel expectedData = context.CDGDiseases.First().To<CDGDiseaseServiceModel>();

			bool actualData = await this.diseasesService.Delete(expectedData.Id);

			Assert.True(actualData, errorMessagePrefix);

		}


		[Fact]
		public async Task Delete_WithCorrectData_ShouldDeleteFromContext()
		{

			string errorMessagePrefix = "CDGDiseasesService Method Delete() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			int idToDelete = context.CDGDiseases.First().To<CDGDiseaseServiceModel>().Id;

			await this.diseasesService.Delete(idToDelete);

			int expectedCount = 1;
			int actualCount = context.CDGDiseases.Count();

			Assert.True(expectedCount == actualCount, errorMessagePrefix);
		}

		[Fact]
		public async Task Delete_WithGivenNonExistenId_ShouldThrowArgumentNullException()
		{


			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.diseasesService = new DiseasesService(context);

			await Assert.ThrowsAsync<ArgumentNullException>(() => this.diseasesService.Delete(120));
		}
	}
}

