using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDGBulgaria.Web.ViewComponents
{
	public class ArticlesUsersViewComponent:ViewComponent
	{
		
		private readonly IArticlesService articlesService;

		public ArticlesUsersViewComponent(IUsersService usersService, IArticlesService articlesService)
		{
			
			this.articlesService = articlesService;
		}

		public  IViewComponentResult Invoke()
		{
			 return this.View(new ArticlesUsersViewComponentViewModel { Users= this.articlesService.GetAllArticlesAuthorsFullnames().Distinct().ToList()});
		}
	}

	public class ArticlesUsersViewComponentViewModel
	{
		public ArticlesUsersViewComponentViewModel()
		{
			this.Users = new HashSet<string>();
		}

		public ICollection<string> Users { get; set; }
	}
}
