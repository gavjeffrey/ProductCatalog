using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductCatalog.Models
{
	//Not really required for this example but I think its important to keep "presentation" seperate from domain model. Technically view model isn't correct term because there is no view in Web API.
	public class AddProductViewModel
    {
		[Required]
		public string Name { get; set; }

		[JsonConverter(typeof(Base64JsonConverter))]
		public byte[] Photo { get; set; }

		[Required]
		[Range(0.01, 1000000000)]
		public decimal Price { get; set; }
	}
}
