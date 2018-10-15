using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Infrastructure;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IRepository<Product> productRepository;
		private readonly IMapper mapper;

		public ProductController(IRepository<Product> productRepository, IMapper mapper)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
		}

		/// <summary>
		/// Use this end point to get a list of products
		/// </summary>
		/// <returns>List of all available products</returns>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var products = await productRepository.GetItems();

			return Ok(products.ToList());
		}

		/// <summary>
		/// Returns a single product
		/// </summary>
		/// <param name="id">The unique identifier for a specific product</param>
		/// <returns>Product</returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var product = await productRepository.GetItem(id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		/// <summary>
		/// Used to create a new product
		/// </summary>
		/// <param name="Product">Details of new Product being added</param>
		[HttpPut]
		public async Task<IActionResult> Put([FromBody]AddProductViewModel Product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var domainProduct = mapper.Map<Product>(Product);
			var newId = await productRepository.AddItem(domainProduct);
			domainProduct.Id = newId;
			return Created(string.Concat(Request.Path.Value, "/", newId), domainProduct);
		}

		/// <summary>
		/// Use to update an existing Product
		/// </summary>
		/// <param name="Product">The Product with changes to be updated</param>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]UpdateProductViewModel Product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await productRepository.UpdateItem(mapper.Map<Product>(Product));
			return Ok(Product);
		}

		/// <summary>
		/// Use to remove a Product from the catalog
		/// </summary>
		/// <param name="id">The unique identifier of the product which should be deleted</param>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await productRepository.DeleteItem(id);

			return NoContent();
		}
	}
}
