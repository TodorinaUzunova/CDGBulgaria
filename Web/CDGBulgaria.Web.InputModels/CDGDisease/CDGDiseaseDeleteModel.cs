using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.InputModels.CDGDisease
{
	public class CDGDiseaseDeleteModel:IMapFrom<CDGDiseaseServiceModel>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
