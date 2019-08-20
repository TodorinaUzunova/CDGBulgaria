using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;

namespace CDGBulgaria.Services.Contracts
{
	public class CDGDiseaseServiceModel:IMapFrom<CDGDisease>, IMapTo<CDGDisease>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}