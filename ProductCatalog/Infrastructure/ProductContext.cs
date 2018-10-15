using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Infrastructure
{
    public class ProductContext: DbContext
	{
		public ProductContext(DbContextOptions<ProductContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>();
		}

		public DbSet<Product> Products { get; set; }
	}
}
