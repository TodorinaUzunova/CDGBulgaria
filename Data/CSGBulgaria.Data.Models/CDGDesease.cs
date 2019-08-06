﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class CDGDesease
	{

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(80)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
