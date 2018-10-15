using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure
{
	public class ProductRepository<T>: IRepository<T>
		 where T : Product
	{
		private readonly ProductContext productRepository;

		public ProductRepository(ProductContext productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task<int> AddItem(T item)
		{
			productRepository.Products.Add(item);

			return await productRepository.SaveChangesAsync();
		}

		public async Task DeleteItem(int id)
		{
			var product = await GetItem(id);
			productRepository.Products.Remove(product);
			await productRepository.SaveChangesAsync();
		}

		public async Task<T> GetItem(int id)
		{
			return await productRepository.Products.SingleOrDefaultAsync(x => x.Id == id) as T;
		}

		public Task<IEnumerable<T>> GetItems()
		{
			return Task.FromResult(
					productRepository.Products as IEnumerable<T>);
		}

		public async Task UpdateItem(T item)
		{
			productRepository.Products.Update(item);
			await productRepository.SaveChangesAsync();
		}
	}
}
