using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using Microsoft.AspNetCore.Mvc;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Web.InputModels.CDGDisease;
using CDGBulgaria.Services.Mapping;
using Microsoft.AspNetCore.Authorization;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
    public class DiseasesController : AdminController
    {
		private readonly IDiseasesService diseasesService;

		public DiseasesController(IDiseasesService diseasesService)
		{
			this.diseasesService = diseasesService;
		}

		[Authorize]
        public async Task<IActionResult> Create(CDGDiseaseCreateInputModel cdgDiseaseCreateInputModel)
        {
			if (!this.ModelState.IsValid)
			{
				return this.View(cdgDiseaseCreateInputModel);
			}

			CDGDiseaseServiceModel cdgDiseaseServiceModel =cdgDiseaseCreateInputModel.To<CDGDiseaseServiceModel>();
			await this.diseasesService.CreateDisease(cdgDiseaseServiceModel);
			return this.Redirect("/Diseases/All");
        }

		[HttpGet(Name = "Edit")]
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
		    CDGDiseaseEditInputModel cdgDiseaseEditInputModel = (await this.diseasesService.GetCDGDiseaseById(id)).To<CDGDiseaseEditInputModel>();

			if (cdgDiseaseEditInputModel == null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(cdgDiseaseEditInputModel));
			}
			return this.View(cdgDiseaseEditInputModel);
		}

		[HttpPost(Name = "Edit")]
		[Authorize]
		public async Task<IActionResult> Edit(int id, CDGDiseaseEditInputModel diseaseEditInputModel)
		{
			
			if (!this.ModelState.IsValid)
			{
				return this.View(diseaseEditInputModel);
			}

		    CDGDiseaseServiceModel diseaseServiceModel = diseaseEditInputModel.To<CDGDiseaseServiceModel>();

			await this.diseasesService.Edit(id, diseaseServiceModel);

			return this.Redirect("/Diseases/All");
		}

		[HttpGet(Name = "Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			
			CDGDiseaseDeleteModel cdgDiseaseDeleteViewModel = (await this.diseasesService.GetCDGDiseaseById(id)).To<CDGDiseaseDeleteModel>();

			if (cdgDiseaseDeleteViewModel == null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(cdgDiseaseDeleteViewModel));
			}

			return this.View(cdgDiseaseDeleteViewModel);
		}

		[HttpPost]
		[Route("/Administration/Diseases/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{

			await this.diseasesService.Delete(id);
			return this.Redirect("/Diseases/All");
		}

	}
}