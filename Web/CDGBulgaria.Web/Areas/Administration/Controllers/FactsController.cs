using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.InputModels.Event;
using CDGBulgaria.Web.InputModels.Fact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
	public class FactsController : AdminController
	{
		private readonly IFactsService factsService;
		private readonly ICloudinaryService cloudinaryService;

		public FactsController(IFactsService factsService, ICloudinaryService cloudinaryService)
		{
			this.factsService = factsService;
			this.cloudinaryService = cloudinaryService;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return this.View();
		}


		[HttpPost(Name = "Create")]
		public async Task<IActionResult> Create(FactCreateInputModel factCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(factCreateInputModel);
			}


			string fileUrl = await this.cloudinaryService.UploadFile(factCreateInputModel.PdfFile, factCreateInputModel.Id.ToString());


			FactServiceModel factServiceModel = new FactServiceModel()
			{
				Id = factCreateInputModel.Id,
				Content = factCreateInputModel.Content,
				PdfFile = fileUrl,
			};

			await this.factsService.Create(factServiceModel);
			return this.Redirect("/");
		}

	}
}