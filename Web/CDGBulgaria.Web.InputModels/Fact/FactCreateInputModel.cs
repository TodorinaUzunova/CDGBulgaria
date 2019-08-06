using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Fact
{
	public class FactCreateInputModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Content is required!")]
		public string Content { get; set; }

		public IFormFile PdfFile { get; set; }
	}
}
