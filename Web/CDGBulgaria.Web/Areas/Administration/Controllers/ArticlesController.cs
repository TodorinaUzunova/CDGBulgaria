using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.InputModels.Article;
using CDGBulgaria.Web.InputModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
	public class ArticlesController:AdminController
	{
		private readonly IArticlesService articlesService;

		public ArticlesController(IArticlesService articlesService)
		{
			this.articlesService = articlesService;
		}

		[HttpGet(Name ="Edit")]
		[Authorize]
		public async Task<IActionResult> Edit(string id)
		{
			ArticleEditInputModel articleEditInputModel = (await this.articlesService.GetArticleById(id)).To<ArticleEditInputModel>();

			if (articleEditInputModel == null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(articleEditInputModel));
			}
			return this.View(articleEditInputModel);
		}

		[HttpPost(Name = "Edit")]
		[Authorize]
		public async Task<IActionResult> Edit(string id, ArticleEditInputModel articleEditInputModel)
		{
			if (id==null)
			{
				return NotFound();
			}
			if (!this.ModelState.IsValid)
			{
				return this.View(articleEditInputModel);
			}

		ArticleServiceModel articleServiceModel = articleEditInputModel.To<ArticleServiceModel>();

			await this.articlesService.Edit(id, articleServiceModel);
			return this.Redirect("/Articles/All");
		}

		[HttpGet(Name = "Delete")]
		public async Task<IActionResult> Delete(string id)
		{
			if (id==null)
			{
				return NotFound();
			}
			ArticleDeleteViewModel articleDeleteViewModel = (await this.articlesService.GetArticleById(id)).To<ArticleDeleteViewModel>();

			if (articleDeleteViewModel==null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(articleDeleteViewModel));
			}

			return this.View(articleDeleteViewModel);
		}

		[HttpPost]
		[Route("/Administration/Articles/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirm(string id)
		{

			await this.articlesService.Delete(id);
			return this.Redirect("/Articles/All");
		}

	}
}
