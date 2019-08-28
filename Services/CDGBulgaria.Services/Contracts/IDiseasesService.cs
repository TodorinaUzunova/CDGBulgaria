using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface IDiseasesService
	{

		Task<bool> CreateDisease(CDGDiseaseServiceModel cdgDiseaseServiceModel);

		IQueryable<CDGDiseaseServiceModel> GetAll();

		Task<CDGDiseaseServiceModel> GetCDGDiseaseById(int id);

		Task<bool> Edit(int id, CDGDiseaseServiceModel serviceModel);

		Task<bool> Delete(int id);
	}
}
