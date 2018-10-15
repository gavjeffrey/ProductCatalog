using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductCatalog.Models
{
	public class UpdateProductViewModel
    {
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[JsonConverter(typeof(Base64JsonConverter))]
		public byte[] Photo { get; set; }

		[Required]
		[Range(0.01, 1000000000)]
		public decimal Price { get; set; }
	}
}
