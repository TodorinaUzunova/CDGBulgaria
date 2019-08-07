using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CDGBulgaria.Services 
{
	public class ArticlesService : IArticlesService
	{
		private readonly CDGBulgariaDbContext context;
		private readonly IHttpContextAccessor contextAccessor;

		public ArticlesService(CDGBulgariaDbContext context, IHttpContextAccessor contextAccessor)
		{
			this.context = context;
			this.contextAccessor = contextAccessor;
		}
		public async Task<bool> CreateArticle(ArticleServiceModel serviceModel)
		{
			string authorId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			Article article = new Article
			{
				Title = serviceModel.Title,
				Content = serviceModel.Content,
				CreatedOn = DateTime.UtcNow,
				AuthorId=authorId,
				Author = new CDGUser {
					FullName=serviceModel.Author.FullName
				}
			};

			await this.context.Articles.AddAsync(article);
			int result = await this.context.SaveChangesAsync();
			return result > 0;
		}

		public IQueryable<ArticleServiceModel> GetAllArticles()
		{
			var allArticles = this.context.Articles.To<ArticleServiceModel>();

			return allArticles;
		}

		public async Task<ArticleServiceModel> GetArticleById(string id)
		{
			ArticleServiceModel article = await this.context.Articles.To<ArticleServiceModel>()
				.SingleOrDefaultAsync(a => a.Id == id);

			return article;
		}

		public async Task<bool> Edit(string id, ArticleServiceModel articleServiceModel)
		{
			Article articleFromDb = await this.context.Articles.SingleOrDefaultAsync(a => a.Id == id);

			if (articleFromDb==null)
			{
				throw new ArgumentNullException(nameof(articleFromDb));
			}

			articleFromDb.Content = articleServiceModel.Content;
			articleFromDb.CreatedOn = DateTime.UtcNow;
			articleFromDb.AuthorId = articleServiceModel.AuthorId;
			

			this.context.Articles.Update(articleFromDb);
			int result = await this.context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<bool> Delete(string id)
		{
			Article articleFromDb = await this.context.Articles.SingleOrDefaultAsync(a => a.Id == id);

			if (articleFromDb == null)
			{
				throw new ArgumentNullException(nameof(articleFromDb));
			}

			this.context.Articles.Remove(articleFromDb);
			int result = await this.context.SaveChangesAsync();

			return result > 0;
		}
	}
}

