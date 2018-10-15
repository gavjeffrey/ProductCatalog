using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Infrastructure
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetItems();
		Task<T> GetItem(int id);
		Task<int> AddItem(T item);
		Task UpdateItem(T item);
		Task DeleteItem(int id);
	}
}
