using System;

namespace ProductCatalog.Models
{
    public class Product
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }
		public decimal Price { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
