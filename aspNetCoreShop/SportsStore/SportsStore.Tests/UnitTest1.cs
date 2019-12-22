using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(e => e.Products).Returns(
                new[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"},
                    new Product {ProductID = 4, Name = "P4"},
                    new Product {ProductID = 5, Name = "P5"}
                }.AsQueryable<Product>());

            ProductController ctr = new ProductController(mock.Object);
            ctr.PageSize = 3;

            //Act
            //Was exception with Microsoft.AspNetCore.Mvc.ViewFeatures package here
            ProductsListViewModel result =
                ctr.List(null, 2).ViewData.Model as ProductsListViewModel;

            //Assert
            Product[] prodArr = result.Products.ToArray();
            Assert.True(prodArr.Length == 2);
            Assert.Equal("P4", prodArr[0].Name);
            Assert.Equal("P5", prodArr[1].Name);

        }

        [Fact]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(e => e.Products).Returns(
                new[]
                {
                    new Product {ProductID = 1, Name = "P1", Category = "Hat"},
                    new Product {ProductID = 2, Name = "P2", Category = "Glasses"},
                    new Product {ProductID = 3, Name = "P3", Category = "Hat"},
                    new Product {ProductID = 4, Name = "P4", Category = "Glasses"},
                    new Product {ProductID = 5, Name = "P5", Category = "Hat"}
                }.AsQueryable<Product>());
            
            ProductController ctr = new ProductController(mock.Object);
            ctr.PageSize = 3;

            //Act
            //Was exception with Microsoft.AspNetCore.Mvc.ViewFeatures package here
            ProductsListViewModel result =
                ctr.List("Hat", 1).ViewData.Model as ProductsListViewModel;

           //Assert
            Product[] prodArr = result.Products.ToArray();
            Assert.True(prodArr.Length == 3);
            Assert.True(prodArr[0].Name == "P1" && prodArr[0].Category == "Hat");
            Assert.True(prodArr[1].Name == "P3" && prodArr[1].Category == "Hat");
            Assert.True(prodArr[2].Name == "P5" && prodArr[2].Category == "Hat");
        }
        
    }
}