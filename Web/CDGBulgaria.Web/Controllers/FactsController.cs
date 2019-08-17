using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Web.ViewModels.Fact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
    public class FactsController : Controller
    {
		private readonly IFactsService factsService;

		public FactsController(IFactsService factsService)
		{
			this.factsService = factsService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var facts = await this.factsService.GetAllFacts()
				.Select(fact => new FactViewModel
				{
					Id=fact.Id,
					Content=fact.Content,
					PdfFile=fact.PdfFile
				})
				.ToListAsync();

			return this.View(facts);
		}
	}
}