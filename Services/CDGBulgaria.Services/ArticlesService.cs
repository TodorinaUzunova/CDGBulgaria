using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CDGBulgaria.Services 
{
	public class ArticlesService : IArticlesService
	{
		private readonly CDGBulgariaDbContext context;

		private const string AtoZAuthorNameOrderCriteria = "authorname-a-to-z";

		private const string ZtoAAuthorNameOrderCriteria = "authorname-z-to-a";

		private const string OldestToNewestDateOrderCriteria = "date-oldest-to-newest";

		private const string NewestToOldestDateOrderCriteria = "date-newest-to-oldest";

		public ArticlesService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}
		public async Task<bool> CreateArticle(ArticleServiceModel serviceModel)
		{
			CDGUser author = await this.context.Users.SingleOrDefaultAsync(u=>u.Id==serviceModel.AuthorId);

			Article article = new Article
			{
				Title = serviceModel.Title,
				Content = serviceModel.Content,
				CreatedOn = DateTime.UtcNow, 
			};

			article.Author = author;
			await this.context.Articles.AddAsync(article);
			int result = await this.context.SaveChangesAsync();
			return result > 0;
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

			articleFromDb.Title = articleServiceModel.Title;
			articleFromDb.Content = articleServiceModel.Content;
		

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

		public IQueryable<ArticleServiceModel> GetAllArticles(string criteria = null)
		{
			switch (criteria)
			{
				case AtoZAuthorNameOrderCriteria: return this.GetAllArticlesByAuthorNameAscending().To<ArticleServiceModel>();
				case ZtoAAuthorNameOrderCriteria: return this.GetAllArticlesByAuthorNameDescending().To<ArticleServiceModel>();
				case OldestToNewestDateOrderCriteria: return this.GetAllArticlesByDateCreatedOnAscending().To<ArticleServiceModel>();
				case NewestToOldestDateOrderCriteria: return this.GetAllArticlesByDateCreatedOnDescending().To<ArticleServiceModel>();
			}

			return this.context.Articles.To<ArticleServiceModel>();
		}

		private IQueryable<Article> GetAllArticlesByAuthorNameAscending()
		{
			return this.context.Articles.OrderBy(article => article.Author.FullName.ToLower());
		}

		private IQueryable<Article> GetAllArticlesByAuthorNameDescending()
		{
			return this.context.Articles.OrderByDescending(article => article.Author.FullName.ToLower());
		}

		private IQueryable<Article> GetAllArticlesByDateCreatedOnAscending()
		{
			return this.context.Articles.OrderBy(article => article.CreatedOn);
		}

		private IQueryable<Article> GetAllArticlesByDateCreatedOnDescending()
		{
			return this.context.Articles.OrderByDescending(article => article.CreatedOn);
		}

		public IEnumerable<string> GetAllArticlesAuthorsFullnames()
		{
			return this.context.Articles.Include(ar=>ar.Author).Select(au => au.Author.FullName).Distinct().ToList();
		}
	}
}

