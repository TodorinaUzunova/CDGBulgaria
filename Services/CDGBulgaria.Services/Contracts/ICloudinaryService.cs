using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface ICloudinaryService
	{

		Task<string> UploadFile(IFormFile file, string fileName);
	}
}
