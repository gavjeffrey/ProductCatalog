using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductCatalog.Controllers;
using ProductCatalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProductCatalog.Tests
{
	[TestClass]
	public class UnitTests_ProductController
	{
		IMapper mapper;

		[TestInitialize]
		public void Initialize()
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			mapper = mappingConfig.CreateMapper();
		}

		[TestMethod]
		public void Test_AddProductWithValidDataIsSucess()
		{
			//arrange
			var productToAddVm = new AddProductViewModel { Name = "Test1", Price = 12.99m };

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.AddItem(It.IsAny<Product>())).Returns(Task.FromResult(12));

			var httpContext = new DefaultHttpContext();

			var controller = new ProductController(mockRepo.Object, mapper)
			{
				ControllerContext = new ControllerContext() { HttpContext = httpContext }
			};

			//act
			var result = controller.Put(productToAddVm).Result as CreatedResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(201, result.StatusCode);
			Assert.AreEqual("/12", result.Location);
		}

		[TestMethod]
		public void Test_AddProductWithInvalidDataReturnsError()
		{
			//arrange
			var productToAddVm = new AddProductViewModel();

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Verify(p => p.AddItem(It.IsAny<Product>()), Times.Never());

			var controller = new ProductController(mockRepo.Object, mapper);
			controller.ModelState.AddModelError("Error", "Error");

			//act
			var result = controller.Put(productToAddVm).Result as BadRequestObjectResult;

			//assert
			mockRepo.VerifyAll();
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void Test_GetWithValidIdReturnsProduct()
		{
			//arrange
			var product = new Product { Id = 12, Name = "Test1", Price = 12.99m };

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.GetItem(12)).Returns(Task.FromResult(product));

			var controller = new ProductController(mockRepo.Object, mapper);

			//act
			var result = controller.Get(12).Result as OkObjectResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(product, result.Value);
		}

		[TestMethod]
		public void Test_GetWithInvalidIdReturnsNotFound()
		{
			//arrange
			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.GetItem(It.IsAny<int>())).Returns(Task.FromResult<Product>(null));

			var controller = new ProductController(mockRepo.Object, mapper);

			//act
			var result = controller.Get(1).Result as NotFoundResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(404, result.StatusCode);
		}

		[TestMethod]
		public void Test_GetReturnsProducts()
		{
			//arrange
			var products = new List<Product> { new Product(), new Product(), new Product() };

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.GetItems()).Returns(Task.FromResult(products as IEnumerable<Product>));

			var controller = new ProductController(mockRepo.Object, mapper);

			//act
			var result = controller.Get().Result as OkObjectResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(products.Count, ((List<Product>)result.Value).Count);
		}

		[TestMethod]
		public void Test_UpdateProductWithValidDataReturnsSuccess()
		{
			//arrange
			var productToUpdateVm = new UpdateProductViewModel { Id = 10, Name = "Test1", Price = 12.99m };

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.UpdateItem(It.IsAny<Product>())).Returns(Task.FromResult(0));

			var controller = new ProductController(mockRepo.Object, mapper);

			//act
			var result = controller.Post(productToUpdateVm).Result as OkObjectResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(productToUpdateVm, result.Value);
		}

		[TestMethod]
		public void Test_UpdateProductWithInvalidDataReturnsError()
		{
			//arrange
			var productToUpdateVm = new UpdateProductViewModel();

			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Verify(p => p.UpdateItem(It.IsAny<Product>()), Times.Never());

			var controller = new ProductController(mockRepo.Object, mapper);
			controller.ModelState.AddModelError("Error", "Error");

			//act
			var result = controller.Post(productToUpdateVm).Result as BadRequestObjectResult;

			//assert
			mockRepo.VerifyAll();
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void Test_DeleteProductReturnsNoContent()
		{
			var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 }, new Product { Id = 3 } };

			//arrange
			var mockRepo = new Mock<Infrastructure.IRepository<Product>>();
			mockRepo.Setup(p => p.DeleteItem(It.IsAny<int>())).Callback<int>((r) => { products.Remove(products.Where(x => x.Id == r).FirstOrDefault()); }).Returns(Task.FromResult(0));

			var controller = new ProductController(mockRepo.Object, mapper);

			//act
			var result = controller.Delete(1).Result as NoContentResult;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(204, result.StatusCode);
			Assert.AreEqual(2, products.Count);
		}
	}
}
