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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CDGBulgariaTests.Service
{

	public class EventsServiceTests
	{
		private IEventsService eventsService;


		public EventsServiceTests()
		{
			MapperInitializer.InitializeMapper(); 
		}
		private List<Event> GetInitialData()
		{
			return new List<Event> {
				new Event {
					Name="CDGHealthMeeting",
					Venue="Sofia",
					Start=DateTime.UtcNow.AddDays(+15),
					MoreInfo="src/pics/something/sofia.pdf"
				  },
				new Event {
					Name="CDGInternalMeeting",
					Venue="Varna",
					Start=DateTime.UtcNow.AddDays(+16),
					MoreInfo="src/pics/something/varna.pdf"
				  },
			};
		}

		private async Task SeedData(CDGBulgariaDbContext context)
		{
			context.Events.AddRange(GetInitialData());
			await context.SaveChangesAsync();

		}

		[Fact]
		public async Task GetAllEvents_WithInitialData_ShouldReturnCorrectResult()
		{
			
			string errorMessagePrefix = "EventsService Method GetAllEvents() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			await SeedData(context);

			this.eventsService = new EventsService(context);

			List<EventServiceModel> actualResults = await this.eventsService.GetAllEvents().ToListAsync();

			List<EventServiceModel> expectedResults = GetInitialData().To<EventServiceModel>().ToList();

			for (int i = 0; i < expectedResults.Count; i++)
			{
				var expectedEntry = expectedResults[i];
				var actualEntry = actualResults[i];

				Assert.True(expectedEntry.Name==actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly");
				Assert.True(expectedEntry.Venue == actualEntry.Venue, errorMessagePrefix + " " + "Venue is not returned properly");
				Assert.True(expectedEntry.Start.ToString("dd/MM/yyyy") == actualEntry.Start.ToString("dd/MM/yyyy"), errorMessagePrefix + " " + "StartDate is not returned properly");
				Assert.True(expectedEntry.MoreInfo == actualEntry.MoreInfo, errorMessagePrefix + " " + "MoreInfo is not returned properly");
			}
			
		}

		[Fact]
		public async Task GetAllEvents_WithZeroData_ShouldReturnEmptyResult()
		{
			
			string errorMessagePrefix = "EventsService Method GetAllEvents() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.eventsService = new EventsService(context);

			List<EventServiceModel> actualResults = await this.eventsService.GetAllEvents().ToListAsync();

				Assert.True(actualResults.Count==0, errorMessagePrefix);
				
		}

		[Fact]
		public async Task Create_WithCorrectData_ShouldSuccesfullyCreate()
		{

			string errorMessagePrefix = "EventsService Method CreateEvent() does not work properly.";

			var context = CDGBulgariaInmemoryFactory.InitializeContext();

			this.eventsService = new EventsService(context);

			EventServiceModel eve = new EventServiceModel()
			{
				Name = "CDGHealthMeeting",
				Venue = "Sofia",
				Start = DateTime.ParseExact("05/03/2019 14:00", "mm/dd/yyyy HH:mm:ss" ,CultureInfo.InvariantCulture),
				MoreInfo = "src/pics/something/sofia.pdf"
			};

			bool actualResult = await this.eventsService.Create(eve);

			Assert.True(actualResult, errorMessagePrefix);

		}
	}
}
