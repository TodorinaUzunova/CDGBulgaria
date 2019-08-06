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

namespace CDGBulgaria.Web.Controllers
{
    public class ArticlesController : Controller
    {
		private readonly IArticlesService articlesService;
		

		public ArticlesController(IArticlesService articleService)
		{
			
			this.articlesService = articleService;
		}

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

			return this.Redirect("/");
		}


		[HttpGet]
		public async Task<IActionResult> All()
		{
			var articles = this.articlesService.GetAllArticles()
	.Select(article => new ArticleViewModel
				{
		            Id=article.Id,
					Content = article.Content,
					CreatedOn = article.CreatedOn,
					AuthorFullName = article.Author.FullName,
				})
				.ToList();

			return this.View(articles);
		}
	}
}