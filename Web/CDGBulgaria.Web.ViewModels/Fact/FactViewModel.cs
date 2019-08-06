using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.Fact
{
	public class FactViewModel:IMapFrom<FactServiceModel>
	{
		public int Id { get; set; }

		public string Content{ get; set; }

		public string PdfFile { get; set; }

	}
}
