using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class FactServiceModel : IMapTo<Fact>, IMapFrom<Fact>
	{

		public int Id { get; set; }

		public string Content { get; set; }

		public string PdfFile { get; set; }
	}
}
