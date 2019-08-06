using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Web.ViewModels.Fact;
using Microsoft.AspNetCore.Mvc;

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
			var facts = this.factsService.GetAllFacts()
				.Select(fact => new FactViewModel
				{
					Id=fact.Id,
					Content=fact.Content,
					PdfFile=fact.PdfFile
				})
				.ToList();

			return this.View(facts);
		}
	}
}