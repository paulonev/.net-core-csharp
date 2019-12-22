using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class NavMenuViewComponentsTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(e => e.Products).Returns(
                new[]
                {
                    new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                    new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                    new Product {ProductID = 4, Name = "P4", Category = "Bananas"},
                }.AsQueryable<Product>());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            
            //Action
            var results = (IEnumerable<string>)
                (target.Invoke() as ViewViewComponentResult).ViewData.Model;
            string[] res = results.ToArray();
            
            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[]
                {"Apples", "Bananas", "Plums"}, res));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(e => e.Products).Returns(
                new[]
                {
                    new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                    new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                    new Product {ProductID = 4, Name = "P4", Category = "Bananas"},
                }.AsQueryable<Product>());

            //View Component
            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext {RouteData = new RouteData()}
            };

            target.RouteData.Values["category"] = categoryToSelect;

            string result = (string) (target.Invoke() as ViewViewComponentResult)
                .ViewData["SelectedCategory"];
            
            Assert.Equal(categoryToSelect, result);
        }
    }
}