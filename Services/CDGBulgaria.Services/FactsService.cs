using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services
{
	public class FactsService : IFactsService
	{
		private readonly CDGBulgariaDbContext dbContext;

		public FactsService(CDGBulgariaDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<bool> Create(FactServiceModel factServiceModel)
		{
			Fact factus = new Fact()
			{
				Id = factServiceModel.Id,
				Content = factServiceModel.Content,
				PdfFile = factServiceModel.PdfFile,

			};

			await dbContext.Facts.AddAsync(factus);
			int result = await dbContext.SaveChangesAsync();

			return result > 0;
		}

		public IQueryable<FactServiceModel> GetAllFacts()
		{
			var allFacts=this.dbContext.Facts
				.Select(fact=>new FactServiceModel
				{
					Id=fact.Id,
					Content=fact.Content,
					PdfFile=fact.PdfFile,
				});

			return allFacts;
		}
	}
}
