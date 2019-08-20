using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.CDGDisease
{
	public class CDGDiseaseCreateInputModel:IMapTo<CDGDiseaseServiceModel>
	{
		[Required(ErrorMessage = "Name is required!")]
		[StringLength(80)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is required!")]
		public string Description { get; set; }
	}
}
