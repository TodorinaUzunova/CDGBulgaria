using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.InputModels.Articles;
using CDGBulgaria.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Controllers
{
	public class ArticlesController : Controller
	{
		private readonly IArticlesService articlesService;


		public ArticlesController(IArticlesService articleService)
		{

			this.articlesService = articleService;
		}

		[Authorize]
		public async Task<IActionResult> Create()
		{

			return this.View();
		}

		[HttpPost(Name = "Create")]
		[Authorize]
		public async Task<IActionResult> Create(ArticleCreateInputModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}
			ArticleServiceModel serviceModel = model.To<ArticleServiceModel>();

			await this.articlesService.CreateArticle(serviceModel);

			return this.Redirect("/Articles/All");
		}

		public async Task<IActionResult> All()
		{
			List<ArticleViewModel> articles = await this.articlesService.GetAllArticles()
						  .Select(article => new ArticleViewModel
						  {
							  Id = article.Id,
							  Title = article.Title,
							  Summary = article.Content.Substring(0,250) + "...",
							  CreatedOn = article.CreatedOn,
							  AuthorFullName = article.Author.FullName,
						  })
					   .ToListAsync();

			return this.View(articles);
		}

		[HttpGet(Name ="Details")]
		[Authorize]
		public async Task<IActionResult> Details(string id)
		{
			if (id==null)
			{
				return NotFound();
			}
			ArticleDetailsViewModel articleViewModel = (await this.articlesService.GetArticleById(id)).To<ArticleDetailsViewModel>();

			return this.View(articleViewModel);
		}


	}
}