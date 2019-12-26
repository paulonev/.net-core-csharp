using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void IndexContainsAllProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"}
                }.AsQueryable<Product>());

            AdminController target = new AdminController(mock.Object);

            Product[] result =
                GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        /// <summary>
        /// Unpacking the result of Action Method
        /// </summary>
        /// <param name="result">Action Method</param>
        /// <typeparam name="T">type of output data</typeparam>
        /// <returns></returns>
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void CanEditProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"}
                }.AsQueryable<Product>());

            AdminController target = new AdminController(mock.Object);

            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void CannotEditNonexistentProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"}
                }.AsQueryable<Product>());

            AdminController target = new AdminController(mock.Object);

            Product result = GetViewModel<Product>(target.Edit(4));
            Assert.Null(result);
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            //imitating storage 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AdminController target = new AdminController(mock.Object) {TempData = tempData.Object};

            Product product = new Product {Name = "NewName"};

            //attempt to save changes to product's name
            IActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);

        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            //imitating storage 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);

            Product product = new Product {Name = "NewName"};

            //add error to the ModelState manually
            target.ModelState.AddModelError("error", "error");
            
            IActionResult result = target.Edit(product);

            mock.Verify(m=>m.SaveProduct(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }
    
    }
}