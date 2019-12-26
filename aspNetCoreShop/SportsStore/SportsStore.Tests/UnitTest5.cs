using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore;
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
            mock.Setup(m=>m.Products).Returns(
                new Product[] {
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
    }
}