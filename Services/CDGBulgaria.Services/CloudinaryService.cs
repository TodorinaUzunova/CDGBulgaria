using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace CDGBulgaria.Services
{
	public class CloudinaryService : ICloudinaryService
	{
		private readonly Cloudinary cloudinaryUtility;

		public CloudinaryService(Cloudinary cloudinaryUtility)
		{
			this.cloudinaryUtility = cloudinaryUtility;
		}


		public async Task<string> UploadFile(IFormFile file, string fileName)
		{

			byte[] destinationData;

			using (var ms=new MemoryStream())
			{
				await file.CopyToAsync(ms);
				destinationData = ms.ToArray();
			}
			UploadResult uploadResult = null;

			using (var ms=new MemoryStream(destinationData))
			{
				ImageUploadParams uploadParams = new ImageUploadParams
					{
					Folder="",//folder name in Cloudinary
					File=new FileDescription(fileName, ms),
				};

				uploadResult = this.cloudinaryUtility.Upload(uploadParams);
			}

			return uploadResult?.SecureUri.AbsoluteUri;
		}
	}
}
