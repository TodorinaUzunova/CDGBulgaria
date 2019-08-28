using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Web.ViewModels.CDGDisease;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
    public class DiseasesController : Controller
    {
		private readonly IDiseasesService diseasesService;

		public DiseasesController(IDiseasesService diseasesService)
		{
			this.diseasesService = diseasesService;
		}

        public async Task< IActionResult> About()
        {
            return View();
        }

		public async Task<IActionResult> Info()
		{
			return View();
		}

		public async Task<IActionResult> Therapies()
		{
			return View();
		}
		public async Task<IActionResult> All()
		{
			var cdgDiseases= await this.diseasesService.GetAll()
				.Select( disease=>new CDGDiseaseViewModel
				{
					Id=disease.Id,
					Name=disease.Name,
					Description=disease.Description,

				}).ToListAsync();

			return View(cdgDiseases);
		}


	}
}