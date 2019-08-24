using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CDGBulgaria.Data.Models
{
	public class Fact
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public string PdfFile { get; set; }
	}
}
 