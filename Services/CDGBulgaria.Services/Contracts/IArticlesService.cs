using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface IArticlesService
   {
		
	    IQueryable<ArticleServiceModel> GetAllArticles(string criteria);


		Task<bool> CreateArticle(ArticleServiceModel serviceModel);

		Task<ArticleServiceModel> GetArticleById(string id);

		Task<bool> Edit(string id, ArticleServiceModel serviceModel);

		Task<bool> Delete(string id);

		IEnumerable<string> GetAllArticlesAuthorsFullnames();


	}
}
 